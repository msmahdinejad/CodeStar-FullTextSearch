
using FullTextsearch.Service.Abstraction;
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
        
        return Ok(new {Name = doc.Result.DocName, Text = doc.Result.Text});
    }
}