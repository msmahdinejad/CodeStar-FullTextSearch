using FullTextsearch.Document;
using FullTextsearch.InvertedIndex;
using FullTextsearch.QueryModel;

namespace FullTextsearch.SearchManager;

public interface ISearchController
{
    SearchStrategyType SearchStrategyName { get; }
    HashSet<ISearchable> SearchWithQuery(IQuery inputQuery, IInvertedIndex myInvertedIndex);
}