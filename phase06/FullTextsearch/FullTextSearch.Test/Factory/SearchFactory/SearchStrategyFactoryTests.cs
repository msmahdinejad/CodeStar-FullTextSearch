using FullTextsearch.Exceptions;
using FullTextsearch.Factory.SearchFactory;
using FullTextsearch.SearchManager;
using Moq;

public class SearchStrategyFactoryTests
{
    private readonly SearchStrategyFactory _searchStrategyFactory;
    private readonly Mock<ISearchController> _mockSignedSearchController;
    public SearchStrategyFactoryTests()
    {
        // Setup mock controllers
        _mockSignedSearchController = new Mock<ISearchController>();
        _mockSignedSearchController.Setup(c => c.SearchStrategyName).Returns(SearchStrategyType.SignedSearch);
        

        // Setup factory with a list of mocked controllers
        _searchStrategyFactory = new SearchStrategyFactory(new List<ISearchController>
        {
            _mockSignedSearchController.Object,
        });
    }

    [Fact]
    public void MakeSearchController_ShouldReturnCorrectController_ForValidSearchStrategyType()
    {
        // Act: Get the controller for SignedSearch
        var result = _searchStrategyFactory.MakeSearchController(SearchStrategyType.SignedSearch);

        // Assert: The correct controller should be returned
        Assert.NotNull(result);
        Assert.Equal(_mockSignedSearchController.Object, result);
    }

    [Fact]
    public void MakeSearchController_ShouldThrowInvalidSearchStrategy_ForInvalidSearchStrategyType()
    {
        // Act & Assert: Verify that an InvalidSearchStrategy exception is thrown for an invalid strategy
        Assert.Throws<InvalidSearchStrategy>(() =>
            _searchStrategyFactory.MakeSearchController((SearchStrategyType)(-2)));
    }
    
}