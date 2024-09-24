using FullTextsearch.Document;
using FullTextsearch.Initialize;
using FullTextsearch.QueryModel;
using FullTextsearch.Service;
using Microsoft.AspNetCore.Http;
using Moq;

namespace FullTextSearch.Test.Service;

public class ApiServiceTests
{
    private readonly ApiService _sut; // System Under Test
    private readonly Mock<ISearchInitializer> _searchInitializerMock;

    public ApiServiceTests()
    {
        _searchInitializerMock = new Mock<ISearchInitializer>();
        _sut = new ApiService(_searchInitializerMock.Object);
    }

    [Fact]
    public void Search_ShouldCallSearchInitializer_WhenQueryTextIsProvided()
    {
        // Arrange
        var queryText = "sample query";
        var expectedResult = new HashSet<ISearchable>();
        _searchInitializerMock.Setup(x => x.Search(It.IsAny<IQuery>())).Returns(expectedResult);

        // Act
        var result = _sut.Search(queryText);

        // Assert
        _searchInitializerMock.Verify(x => x.Search(It.IsAny<IQuery>()), Times.Once);
        Assert.Equal(expectedResult, result);
    }

    [Fact]
    public async Task AddDocument_ShouldReadFileContentAndAddDocument()
    {
        // Arrange
        var fileName = "test.txt";
        var fileContent = "This is a test document.";
        var fileMock = new Mock<IFormFile>();
        var stream = new MemoryStream();
        var writer = new StreamWriter(stream);
        await writer.WriteAsync(fileContent);
        await writer.FlushAsync();
        stream.Position = 0;

        fileMock.Setup(f => f.FileName).Returns(fileName);
        fileMock.Setup(f => f.OpenReadStream()).Returns(stream);

        var documentMock = new Mock<ISearchable>();
        _searchInitializerMock.Setup(x => x.AddData(It.IsAny<ISearchable>())).Verifiable();

        // Act
        var result = await _sut.AddDocument(fileMock.Object);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(fileName, result.DocName);
        Assert.Equal(fileContent, result.Text);
        _searchInitializerMock.Verify(x => x.AddData(It.IsAny<ISearchable>()), Times.Once);
    }
}