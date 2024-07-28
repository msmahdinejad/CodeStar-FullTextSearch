namespace phase02;

public class IntersectResultList : ISearchResultList
{
    private static IntersectResultList _intersectResultList;
    public static IntersectResultList Instance => _intersectResultList ??= new IntersectResultList();
    public HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex)
    {
        HashSet<ISearchable> resultList = new HashSet<ISearchable>();
        if (keyList.Count >= 1)
        {
            resultList = myInvertedIndex.GetValue(keyList.First());
            foreach (var word in keyList)
            {
                resultList.IntersectWith(myInvertedIndex.GetValue(word));
            }
        }
        return resultList;
    }
    
}