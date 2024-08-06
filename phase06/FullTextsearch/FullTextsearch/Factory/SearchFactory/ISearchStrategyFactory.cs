using FullTextsearch.SearchManager;

namespace FullTextsearch.Factory.SearchFactory;

public interface ISearchStrategyFactory
{
    ISearchController MakeSearchController(SearchStrategyType searchType);
}