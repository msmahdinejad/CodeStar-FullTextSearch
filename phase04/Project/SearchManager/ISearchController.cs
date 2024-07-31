using phase02.Document;
using phase02.InvertedIndex;
using phase02.QueryModel;

namespace phase02.SearchManager;

public interface ISearchController
{
    SearchStrategyType SearchStrategyName { get; }
    HashSet<ISearchable> SearchWithQuery(IQuery inputQuery, IInvertedIndex myInvertedIndex);
}