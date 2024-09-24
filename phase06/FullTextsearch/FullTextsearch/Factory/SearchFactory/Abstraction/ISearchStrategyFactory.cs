using FullTextsearch.SearchManager;
using FullTextsearch.SearchManager.Abstraction;

namespace FullTextsearch.Factory.SearchFactory.Abstraction;

public interface ISearchStrategyFactory
{
    ISearchController MakeSearchController(SearchStrategyType searchType);
}