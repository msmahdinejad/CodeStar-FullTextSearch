using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;
using FullTextsearch.InvertedIndex;
using FullTextsearch.InvertedIndex.Abstraction;
using FullTextsearch.SearchManager.ResultList.Abstraction;

namespace FullTextsearch.SearchManager.ResultList;

public class UnionResultListMaker : IResultListMaker
{
    public ResultListMakerType Type { get; init; } = ResultListMakerType.Union;
    public HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex)
    {
        var resultList = new HashSet<ISearchable>();
        
        if (keyList.Count < 1) return resultList;

        return keyList
            .SelectMany(myInvertedIndex.GetValue)
            .ToHashSet();
    }
    
}