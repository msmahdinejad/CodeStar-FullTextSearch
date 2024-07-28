namespace phase02;

public class IntersectResultListMaker : IResultListMaker
{
    private static IntersectResultListMaker _intersectResultListMaker;
    public static IntersectResultListMaker Instance => _intersectResultListMaker ??= new IntersectResultListMaker();

    public HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex)
    {
        HashSet<ISearchable> resultList = new HashSet<ISearchable>();

        if (keyList.Count >= 1)
        {
            resultList = keyList
                .Skip(1)
                .Aggregate(
                    new HashSet<ISearchable>(myInvertedIndex.GetValue(keyList.First())),
                    (currentSet, word) =>
                    {
                        currentSet.IntersectWith(myInvertedIndex.GetValue(word));
                        return currentSet;
                    });
        }

        return resultList;
    }
}