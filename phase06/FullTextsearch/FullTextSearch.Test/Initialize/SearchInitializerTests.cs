using FullTextsearch.Document;
using FullTextsearch.Document.Extractor;
using FullTextsearch.Factory.SearchFactory;
using FullTextsearch.Initialize;
using FullTextsearch.InvertedIndex;
using FullTextsearch.QueryModel;
using FullTextsearch.SearchManager;
using Moq;

namespace FullTextSearch.Test.Initialize;

public class SearchInitializerTests
{
    private readonly Mock<IInvertedIndex> _mockInvertedIndex;
    private readonly Mock<ISearchStrategyFactory> _mockSearchStrategyFactory;
    private readonly Mock<IExtractor> _mockExtractor;
    private readonly Mock<ISearchController> _mockSearchController;
    private readonly SearchInitializer _searchInitializer;

    public SearchInitializerTests()
    {
        // Create mocks for dependencies
        _mockInvertedIndex = new Mock<IInvertedIndex>();
        _mockSearchStrategyFactory = new Mock<ISearchStrategyFactory>();
        _mockExtractor = new Mock<IExtractor>();
        _mockSearchController = new Mock<ISearchController>();

        // Setup the SearchInitializer with mocked dependencies
        _searchInitializer = new SearchInitializer(
            _mockInvertedIndex.Object,
            _mockSearchStrategyFactory.Object,
            _mockExtractor.Object);
    }

    [Fact]
    public void Build_ShouldSetSearchController_WhenValidSearchTypeProvided()
    {
        // Arrange
        var searchType = SearchStrategyType.SignedSearch;
        _mockSearchStrategyFactory
            .Setup(factory => factory.MakeSearchController(searchType))
            .Returns(_mockSearchController.Object);

        // Act
        _searchInitializer.Build(searchType);

        // Assert
        Assert.NotNull(_searchInitializer.SearchController);
        Assert.Equal(_mockSearchController.Object, _searchInitializer.SearchController);
    }

    [Fact]
    public void Search_ShouldInvokeSearchWithQuery_WhenCalled()
    {
        // Arrange
        var query = new Mock<IQuery>();
        var expectedResults = new HashSet<ISearchable>();
        
        _mockSearchStrategyFactory
            .Setup(factory => factory.MakeSearchController(It.IsAny<SearchStrategyType>()))
            .Returns(_mockSearchController.Object);
        
        _mockSearchController
            .Setup(controller => controller.SearchWithQuery(query.Object, _mockInvertedIndex.Object))
            .Returns(expectedResults);

        _searchInitializer.Build(SearchStrategyType.SignedSearch); // Ensure the SearchController is built

        // Act
        var results = _searchInitializer.Search(query.Object);

        // Assert
        Assert.Equal(expectedResults, results);
        _mockSearchController.Verify(controller => controller.SearchWithQuery(query.Object, _mockInvertedIndex.Object), Times.Once);
    }

    [Fact]
    public void AddData_ShouldInvokeAddDataToMap_WhenCalled()
    {
        // Arrange
        var data = new Mock<ISearchable>();

        // Act
        _searchInitializer.AddData(data.Object);

        // Assert
        _mockInvertedIndex.Verify(index => index.AddDataToMap(data.Object, _mockExtractor.Object), Times.Once);
    }
}