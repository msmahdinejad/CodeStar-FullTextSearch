using FullTextsearch.Document;
using FullTextsearch.InvertedIndex;

namespace FullTextsearch.SearchManager.ResultList;

public interface IResultListMaker
{
    HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex);
}