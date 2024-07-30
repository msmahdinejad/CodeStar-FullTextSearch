using phase02.Document;
using phase02.InvertedIndex;
using phase02.QueryModel;

namespace phase02.SearchManager;

public interface ISearchStrategy
{
    SearchStrategyType SearchStrategyName { get; }
    HashSet<ISearchable> SearchWithQuery(Query inputQuery, IInvertedIndex myInvertedIndex);
}