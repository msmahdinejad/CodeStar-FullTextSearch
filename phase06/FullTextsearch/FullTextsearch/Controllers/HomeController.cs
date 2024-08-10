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
using FullTextsearch.Service;
using Microsoft.AspNetCore.Mvc;

namespace FullTextsearch.Controllers;


[Route("[controller]/[action]")]
[ApiController]
public class HomeController : ControllerBase
{
    private readonly IApiService _apiService;

    public HomeController(IApiService apiService)
    {
        _apiService = apiService;
    }


    [HttpGet("{queryText}")]
    public IActionResult SearchWithQuery([FromRoute]string queryText)
    {
        var result = _apiService.Search(queryText);
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

        var doc = _apiService.AddDocument(file);
        
        return Ok(doc);
    }
}