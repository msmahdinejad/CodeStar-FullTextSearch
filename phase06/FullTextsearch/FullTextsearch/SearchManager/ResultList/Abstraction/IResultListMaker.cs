using FullTextsearch.Document.Abstraction;
using FullTextsearch.InvertedIndex.Abstraction;

namespace FullTextsearch.SearchManager.ResultList.Abstraction;

public interface IResultListMaker
{
    ResultListMakerType Type { get; init; }
    HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex);
}