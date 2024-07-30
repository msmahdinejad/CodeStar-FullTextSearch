using phase02.Document;
using phase02.InvertedIndex;

namespace phase02.SearchManager.ResultList;

public class UnionResultListMaker : IResultListMaker
{
    private static UnionResultListMaker _unionResultListMaker;
    public static UnionResultListMaker Instance => _unionResultListMaker ??= new UnionResultListMaker();

    public HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex)
    {
        var resultList = new HashSet<ISearchable>();
        
        if (keyList.Count < 1) return resultList;

        return keyList
            .SelectMany(myInvertedIndex.GetValue)
            .ToHashSet();
    }
    
}