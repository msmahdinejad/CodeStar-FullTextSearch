using phase02.Exceptions;
using phase02.SearchManager;

namespace phase02.Factory.SearchFactory;

public class SearchStrategyFactory : ISearchStrategyFactory
{
    private List<ISearchController> _searchStrategyList { get; set; }
    public SearchStrategyFactory(List<ISearchController> searchStrategyList) => _searchStrategyList = searchStrategyList;

    public ISearchController MakeSearchController(SearchStrategyType searchType)
    {
        var strategy = _searchStrategyList.SingleOrDefault(x => x.SearchStrategyName == searchType);
        return strategy ?? throw new InvalidSearchStrategy();
    }
}