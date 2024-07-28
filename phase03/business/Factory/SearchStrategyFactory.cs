namespace phase02;
public class SearchStrategyFactory : ISearchStrategyFactory
{
    private List<ISearchStrategy> _searchStrategyList { get; set; }
    public SearchStrategyFactory(List<ISearchStrategy> searchStrategyList) => _searchStrategyList = searchStrategyList;
    public ISearchStrategy makeSearchcontroller(string searchType)
    {
        var strategy = _searchStrategyList.SingleOrDefault(x => x.GetSearchStrategyName() == searchType);
        return strategy ??throw new Exception("Search Strategy not found!");
    }
}