namespace phase02;

public class UnionResultListMaker : IResultListMaker
{
    private static UnionResultListMaker _unionResultListMaker;
    public static UnionResultListMaker Instance => _unionResultListMaker ??= new UnionResultListMaker();

    public HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex)
    {
        HashSet<ISearchable> resultList = new HashSet<ISearchable>();
        if (keyList.Count >= 1)
        {
            resultList = keyList
                .SelectMany(word => myInvertedIndex.GetValue(word))
                .ToHashSet();
        }

        return resultList;
    }
}