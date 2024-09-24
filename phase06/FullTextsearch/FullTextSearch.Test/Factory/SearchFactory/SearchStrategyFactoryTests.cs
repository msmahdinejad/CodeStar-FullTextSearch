using FullTextsearch.Exceptions;
using FullTextsearch.Factory.SearchFactory;
using FullTextsearch.SearchManager;
using FullTextsearch.SearchManager.Abstraction;
using Moq;

namespace FullTextSearch.Test.Factory.SearchFactory;

public class SearchStrategyFactoryTests
{
    private readonly SearchStrategyFactory _searchStrategyFactory;
    private readonly Mock<ISearchController> _mockSignedSearchController;
    public SearchStrategyFactoryTests()
    {
        _mockSignedSearchController = new Mock<ISearchController>();
        _mockSignedSearchController.Setup(c => c.SearchStrategyName).Returns(SearchStrategyType.SignedSearch);
        
        _searchStrategyFactory = new SearchStrategyFactory(new List<ISearchController>
        {
            _mockSignedSearchController.Object,
        });
    }

    [Fact]
    public void MakeSearchController_ShouldReturnCorrectController_ForValidSearchStrategyType()
    {
        // Act
        var result = _searchStrategyFactory.MakeSearchController(SearchStrategyType.SignedSearch);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(_mockSignedSearchController.Object, result);
    }

    [Fact]
    public void MakeSearchController_ShouldThrowInvalidSearchStrategy_ForInvalidSearchStrategyType()
    {
        // Act & Assert
        Assert.Throws<InvalidSearchStrategy>(() =>
            _searchStrategyFactory.MakeSearchController((SearchStrategyType)(-2)));
    }
    
}