namespace phase02;
public interface ISearchStrategyFactory
{
    ISearchStrategy makeSearchcontroller(InvertedIndexController myInvertedIndex, string searchType);
}