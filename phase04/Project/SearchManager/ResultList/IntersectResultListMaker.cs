using phase02.Document;
using phase02.InvertedIndex;

namespace phase02.SearchManager.ResultList;

public class IntersectResultListMaker : IResultListMaker
{
    private static IntersectResultListMaker _intersectResultListMaker;
    public static IntersectResultListMaker Instance => _intersectResultListMaker ??= new IntersectResultListMaker();

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