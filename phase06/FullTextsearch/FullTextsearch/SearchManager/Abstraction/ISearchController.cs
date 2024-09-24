using FullTextsearch.Document.Abstraction;
using FullTextsearch.InvertedIndex.Abstraction;
using FullTextsearch.QueryModel.Abstraction;

namespace FullTextsearch.SearchManager.Abstraction;

public interface ISearchController
{
    SearchStrategyType SearchStrategyName { get; }
    HashSet<ISearchable> SearchWithQuery(IQuery inputQuery, IInvertedIndex myInvertedIndex);
}