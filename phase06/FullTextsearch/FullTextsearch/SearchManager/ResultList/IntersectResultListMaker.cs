using FullTextsearch.Document;
using FullTextsearch.InvertedIndex;

namespace FullTextsearch.SearchManager.ResultList;

public class IntersectResultListMaker : IResultListMaker
{
    public HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex)
    {
        HashSet<ISearchable> resultList = new HashSet<ISearchable>();

        if (keyList.Count < 1) return resultList;

        resultList = myInvertedIndex.GetValue(keyList.First());

        foreach (var word in keyList)
        {
            resultList.IntersectWith(myInvertedIndex.GetValue(word));
        }

        return resultList;
    }
}