using phase02.SearchManager;

namespace phase02.Factory.SearchFactory;

public interface ISearchStrategyFactory
{
    ISearchStrategy MakeSearchController(SearchStrategyType searchType);
}