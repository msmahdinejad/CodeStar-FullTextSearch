namespace phase02;

public class UnionResultList : ISearchResultList
{
    private static UnionResultList _unionResultList;
    public static UnionResultList Instance => _unionResultList ??= new UnionResultList();
    public HashSet<ISearchable> MakeResultList(HashSet<string> keyList, InvertedIndexController myInvertedIndex)
    {
        HashSet<ISearchable> resultList = new HashSet<ISearchable>();
        if (keyList.Count >= 1)
        {
            resultList = myInvertedIndex.GetValue(keyList.First());
            foreach (var word in keyList)
            {
                resultList.UnionWith(myInvertedIndex.GetValue(word));
            }
        }
        return resultList;
    }
    
}