namespace phase02;

public interface ISearchStrategyFactory
{
    ISearchStrategy MakeSearchController(string searchType);
}