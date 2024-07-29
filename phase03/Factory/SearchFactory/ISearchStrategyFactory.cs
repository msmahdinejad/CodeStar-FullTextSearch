namespace phase02;

public interface ISearchStrategyFactory
{
    ISearchStrategy MakeSearchController(SearchStrategyType searchType);
}