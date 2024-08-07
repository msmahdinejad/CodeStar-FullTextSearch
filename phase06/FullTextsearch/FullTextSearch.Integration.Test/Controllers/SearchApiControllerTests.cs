using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;

namespace FullTextSearch.Integration.Test.Controllers;

public class SearchApiControllerTests(WebApplicationFactory<Program> factory)
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client = factory.CreateClient();


    [Fact]
    public async Task SearchWithQuery_ReturnsResults_WhenQueryIsValid()
    {
        // Arrange
        var queryText = "have";
        var requestUri = $"/FullTextSearch/Search/{queryText}";

        // Act
        var response = await _client.GetAsync(requestUri);
        response.EnsureSuccessStatusCode();
        var content = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.Contains("57110", content);
        Assert.Contains("58043", content);
        Assert.Contains("58044", content);
    }

    [Fact]
    public async Task SearchWithQuery_ReturnsNotFound_WhenQueryIsInvalid()
    {
        // Arrange
        var queryText = "xxxxxxxxxxxxx";
        var requestUri = $"/FullTextSearch/Search/{queryText}";

        // Act
        var response = await _client.GetAsync(requestUri);

        // Assert
        Assert.Equal(System.Net.HttpStatusCode.NotFound, response.StatusCode);
    }
}