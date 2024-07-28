namespace phase02;
public interface ISearchStrategyFactory
{
    ISearchStrategy makeSearchcontroller(string searchType);
}