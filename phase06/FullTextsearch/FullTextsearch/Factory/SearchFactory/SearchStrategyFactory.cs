using FullTextsearch.Exceptions;
using FullTextsearch.Factory.SearchFactory.Abstraction;
using FullTextsearch.SearchManager;
using FullTextsearch.SearchManager.Abstraction;

namespace FullTextsearch.Factory.SearchFactory;

public class SearchStrategyFactory : ISearchStrategyFactory
{
    private IEnumerable<ISearchController> _searchStrategyList { get; }
    public SearchStrategyFactory(IEnumerable<ISearchController> searchStrategyList) => _searchStrategyList = searchStrategyList;

    public ISearchController MakeSearchController(SearchStrategyType searchType)
    {
        var strategy = _searchStrategyList.SingleOrDefault(x => x.SearchStrategyName == searchType);
        return strategy ?? throw new InvalidSearchStrategy();
    }
}