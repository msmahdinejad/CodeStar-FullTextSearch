using FullTextsearch.Context;
using FullTextsearch.Document;
using FullTextsearch.Document.Extractor;
using FullTextsearch.Document.Formater;
using FullTextsearch.Factory.FolderFactory;
using FullTextsearch.Factory.SearchFactory;
using FullTextsearch.Initialize;
using FullTextsearch.InvertedIndex;
using FullTextsearch.QueryManager.WordFinder;
using FullTextsearch.QueryModel;
using FullTextsearch.SearchManager;
using FullTextsearch.SearchManager.ResultList;
using FullTextsearch.SearchManager.SignedSearchManager;
using Microsoft.AspNetCore.Mvc;

namespace FullTextsearch.Controllers;


[Route("FullTextSearch/Search")]
[ApiController]
public class SearchApiController : ControllerBase
{
    private readonly ApplicationDbContext _context;
    private readonly ISearchInitializer _searchInitializer;
    
    public SearchApiController(ApplicationDbContext context, ISearchInitializer searchInitializer)
    {
        _context = context;
        _searchInitializer = searchInitializer;
        
        _searchInitializer.Build(DataType.Document, resources.Resources.FolderPath, SearchStrategyType.SignedSearch);
    }
    
    [HttpGet("{queryText}")]
    public IActionResult SearchWithQuery([FromRoute]string queryText)
    {
        var query = new AdvancedQuery(queryText);
        var result = _searchInitializer.Search(query);
        if (result.Count == 0)
        {
            return NotFound();
        }
        return Ok(result.Select(x => x.GetValue()));
    }
}