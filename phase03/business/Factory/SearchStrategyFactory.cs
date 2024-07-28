namespace phase02;
public class SearchStrategyFactory : ISearchStrategyFactory
{
    private static SearchStrategyFactory _searchStrategyFactory;
    public static SearchStrategyFactory Instance => _searchStrategyFactory ??= new SearchStrategyFactory();
    private Dictionary<string, ISearchStrategy> _map { get; set; }

    public ISearchStrategy makeSearchcontroller(InvertedIndexController myInvertedIndex, string searchType)
    {
        _map = new Dictionary<string, ISearchStrategy>()
        {
            {"SignedSearch", new SignedSearchStrategy(myInvertedIndex)}
        };
        if (_map.ContainsKey(searchType))
            return _map[searchType];
        else
            throw new Exception("Search Strategy not found!");
        
    }
}