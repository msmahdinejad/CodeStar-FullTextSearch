using phase02.Exceptions;
using phase02.SearchManager;

namespace phase02.Factory.SearchFactory;

public class SearchStrategyFactory : ISearchStrategyFactory
{
    private List<ISearchStrategy> _searchStrategyList { get; set; }
    public SearchStrategyFactory(List<ISearchStrategy> searchStrategyList) => _searchStrategyList = searchStrategyList;

    public ISearchStrategy MakeSearchController(SearchStrategyType searchType)
    {
        var strategy = _searchStrategyList.SingleOrDefault(x => x.SearchStrategyName == searchType);
        return strategy ?? throw new InvalidSearchStrategy();
    }
}