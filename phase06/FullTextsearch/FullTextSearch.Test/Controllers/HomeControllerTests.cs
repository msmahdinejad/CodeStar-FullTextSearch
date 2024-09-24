using FullTextsearch.Controllers;
using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;
using FullTextsearch.Document.Formater;
using FullTextsearch.Service;
using FullTextsearch.Service.Abstraction;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;

namespace FullTextSearch.Test.Controllers
{
    public class HomeControllerTests
    {
        private readonly Mock<IApiService> _apiServiceMock;
        private readonly HomeController _controller;

        public HomeControllerTests()
        {
            _apiServiceMock = new Mock<IApiService>();
            _controller = new HomeController(_apiServiceMock.Object);
        }

        [Fact]
        public void SearchWithQuery_ShouldReturnOk_WhenResultsFound()
        {
            // Arrange
            var queryText = "example";
            var searchResult = new HashSet<ISearchable>
            {
                new Mock<ISearchable>().Object
            };
            _apiServiceMock.Setup(x => x.Search(queryText)).Returns(searchResult);

            // Act
            var result = _controller.SearchWithQuery(queryText) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
            Assert.Single(result.Value as IEnumerable<string>);
        }

        [Fact]
        public void SearchWithQuery_ShouldReturnNotFound_WhenNoResults()
        {
            // Arrange
            var queryText = "nonexistent";
            var searchResult = new HashSet<ISearchable>();
            _apiServiceMock.Setup(x => x.Search(queryText)).Returns(searchResult);

            // Act
            var result = _controller.SearchWithQuery(queryText) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status404NotFound, result.StatusCode);
        }

        [Fact]
        public async Task UploadFile_ShouldReturnOk_WhenFileIsUploaded()
        {
            // Arrange
            var fileMock = new Mock<IFormFile>();
            var fileName = "test.txt";
            var fileContent = "This is a test file.";
            var content = new System.IO.MemoryStream(System.Text.Encoding.UTF8.GetBytes(fileContent));
            fileMock.Setup(f => f.FileName).Returns(fileName);
            fileMock.Setup(f => f.Length).Returns(content.Length);
            fileMock.Setup(f => f.OpenReadStream()).Returns(content);

            var documentMock = new Mock<FullTextsearch.Document.Document>("test.txt", fileContent, new DocumentTextEditor());
            _apiServiceMock.Setup(x => x.AddDocument(fileMock.Object)).ReturnsAsync(documentMock.Object);

            // Act
            var result = await _controller.UploadFile(fileMock.Object) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status200OK, result.StatusCode);
        }

        [Fact]
        public async Task UploadFile_ShouldReturnBadRequest_WhenNoFileUploaded()
        {
            // Arrange
            IFormFile fileMock = null; 

            // Act
            var result = await _controller.UploadFile(fileMock) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(StatusCodes.Status400BadRequest, result.StatusCode);
            Assert.Equal("No file uploaded.", result.Value);
        }
    }
}
