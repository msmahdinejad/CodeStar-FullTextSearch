namespace phase02;

public interface ISearchStrategy
{
    SearchStrategyType SearchStrategyName { get; }
    HashSet<ISearchable> SearchWithQuery(Query inputQuery, IInvertedIndex myInvertedIndex);
}