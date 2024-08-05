using phase02.SearchManager;

namespace phase02.Factory.SearchFactory;

public interface ISearchStrategyFactory
{
    ISearchController MakeSearchController(SearchStrategyType searchType);
}