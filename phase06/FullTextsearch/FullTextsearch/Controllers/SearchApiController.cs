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


[Route("api/Home/[action]")]
[ApiController]
public class SearchApiController : ControllerBase
{
    private readonly ISearchInitializer _searchInitializer;
    
    public SearchApiController(ISearchInitializer searchInitializer)
    {
        _searchInitializer = searchInitializer;
        
        _searchInitializer.Build(SearchStrategyType.SignedSearch);
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
    
    [HttpPost("upload")]
    public async Task<IActionResult> UploadFile(IFormFile file)
    {
        if (file == null || file.Length == 0)
        {
            return BadRequest("No file uploaded.");
        }

        var fileName = file.FileName;
        
        string fileContent;
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            fileContent = await reader.ReadToEndAsync();
        }

        var data = new Document.Document(fileName, fileContent, new DocumentTextEditor());
        
        _searchInitializer.AddData(data);
        
        return Ok(new 
        {
            FileName = fileName,
            Content = fileContent
        });
    }
}