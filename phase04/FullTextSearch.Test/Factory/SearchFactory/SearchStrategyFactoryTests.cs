using phase02.Exceptions;
using phase02.Factory.SearchFactory;
using phase02.SearchManager;

namespace FullTextSearch.Test.Factory.SearchFactory;

public class SearchStrategyFactoryTests
{
    private readonly SearchStrategyFactory _sut;

    public SearchStrategyFactoryTests()
    {
        _sut = new SearchStrategyFactory([new SignedSearchStrategy()]);
    }

    [Fact]
    public void MakeSearchController_ShouldReturnsCorrectSearchController_WhenSearchStrategyTypeIsOk()
    {
        //Arrange
        var searchStrategy = SearchStrategyType.SignedSearch;
        var searchController = new SignedSearchStrategy();
        
        //Act
        var result = _sut.MakeSearchController(searchStrategy);

        //Assert
        Assert.Equal(result.GetType(), searchController.GetType());
    }
    
    [Fact]
    public void MakeSearchController_ShouldReturnsInvalidSearchStrategyException_WhenSearchStrategyTypeIsNotOk()
    {
        //Arrange
        var searchStrategy = SearchStrategyType.WrongType;

        //Act
        var action = () => _sut.MakeSearchController(searchStrategy);

        //Assert
        Assert.Throws<InvalidSearchStrategy>(action);
    }
}