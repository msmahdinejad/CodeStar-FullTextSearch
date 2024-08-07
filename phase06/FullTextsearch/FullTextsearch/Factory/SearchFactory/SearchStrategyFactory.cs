using FullTextsearch.Exceptions;
using FullTextsearch.SearchManager;

namespace FullTextsearch.Factory.SearchFactory;

public class SearchStrategyFactory : ISearchStrategyFactory
{
    private IEnumerable<ISearchController> _searchStrategyList { get; set; }
    public SearchStrategyFactory(IEnumerable<ISearchController> searchStrategyList) => _searchStrategyList = searchStrategyList;

    public ISearchController MakeSearchController(SearchStrategyType searchType)
    {
        var strategy = _searchStrategyList.SingleOrDefault(x => x.SearchStrategyName == searchType);
        return strategy ?? throw new InvalidSearchStrategy();
    }
}