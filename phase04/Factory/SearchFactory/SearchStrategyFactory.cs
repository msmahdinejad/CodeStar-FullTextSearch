using phase02.Exceptions;

namespace phase02;

public class SearchStrategyFactory : ISearchStrategyFactory
{
    private List<ISearchStrategy> _searchStrategyList { get; set; }
    public SearchStrategyFactory(List<ISearchStrategy> searchStrategyList, SignedSearchStrategy signedSearchStrategy) => _searchStrategyList = searchStrategyList;

    public ISearchStrategy MakeSearchController(SearchStrategyType searchType)
    {
        var strategy = _searchStrategyList.SingleOrDefault(x => x.SearchStrategyName == searchType);
        return strategy ?? throw new InvalidSearchStrategy();
    }
}