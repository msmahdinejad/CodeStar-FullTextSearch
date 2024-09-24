using System.Net.Http.Headers;
using System.Text;
using FullTextsearch.Context;
using FullTextsearch.Model;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;

namespace FullTextSearch.Integration.Test.Controllers;

public class HomeControllerTests : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public HomeControllerTests(WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }



    [Fact]
    public async Task SearchWithQuery_ReturnsResults_WhenQueryIsValid()
    {
        // Arrange
        // using (var scope = factory.Services.CreateScope())
        // {
        //     var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        //
        //     var value = new InvertedIndexRecord
        //     {
        //         Key = "have",
        //         Values = ["57110"]
        //     };
        //
        //     dbContext.InvertedIndexMap.Add(value);
        //     await dbContext.SaveChangesAsync();
        // }
        
        var queryText = "have";
        var requestUri = $"/Home/SearchWithQuery/{queryText}";

        // Act
        var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("57110", content);
    }

    [Fact]
    public async Task SearchWithQuery_ReturnsNotFound_WhenQueryIsInvalid()
    {
        // Arrange
        var queryText = "xxxxxxxxxxxxx";
        var requestUri = $"/Home/SearchWithQuery/{queryText}";

        // Act
        var response = await _client.GetAsync(requestUri);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
    
    [Fact]
    public async Task UploadFile_ShouldReturnsCurrentDoc_WhenFileIsValid()
    {
        // Arrange
        var fileName = "test.txt";
        var contentType = "text/plain";
        var fileContents = Encoding.UTF8.GetBytes("This is a test file.");

        var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(fileContents);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(fileContent, "file", fileName);

        // Act
        var response = await _client.PostAsync("/Home/UploadFile/upload", content);

        // Assert
        Assert.True(response.IsSuccessStatusCode, "Response status code does not indicate success.");
        
        var responseString = await response.Content.ReadAsStringAsync();

        Assert.Contains("test.txt", responseString);
        Assert.Contains("This is a test file.", responseString);
    }
    
    [Fact]
    public async Task UploadFile_WithEmptyFile_ReturnsBadRequest()
    {
        // Arrange
        var fileName = "empty.txt";
        var contentType = "text/plain";
        var fileContents = new byte[0];

        var content = new MultipartFormDataContent();
        var fileContent = new ByteArrayContent(fileContents);
        fileContent.Headers.ContentType = new MediaTypeHeaderValue(contentType);
        content.Add(fileContent, "file", fileName);

        // Act
        var response = await _client.PostAsync("/Home/UploadFile/upload", content);

        // Assert
        Assert.False(response.IsSuccessStatusCode, "Expected response status code to indicate failure.");
        Assert.Equal(System.Net.HttpStatusCode.BadRequest, response.StatusCode);
        
        var responseString = await response.Content.ReadAsStringAsync();
        Assert.Contains("No file uploaded", responseString);
    }
}