using FullTextsearch.Document;
using FullTextsearch.InvertedIndex;

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