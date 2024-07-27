namespace phase02;

public class SignedSearch
{
    public HashSet<ISearchable> UnSignedResult { get; set; }
    public HashSet<ISearchable> PositiveResult { get; set; }
    public HashSet<ISearchable> NegativeResult { get; set; }
    public InvertedIndexController MyInvertedIndex { get; init; }
    public SignedSearch(InvertedIndexController myInvertedIndex)
    {
        UnSignedResult = new HashSet<ISearchable>();
        PositiveResult = new HashSet<ISearchable>();
        NegativeResult = new HashSet<ISearchable>();
        MyInvertedIndex = myInvertedIndex;
    }
    public HashSet<ISearchable> MakeResultList(HashSet<string> keyList)
    {
        HashSet<ISearchable> resultList = new HashSet<ISearchable>();
        if (keyList.Count >= 1)
        {
            resultList = SearchInMap(keyList.First());
            foreach (var word in keyList)
            {
                resultList.IntersectWith(SearchInMap(word));
            }
        }
        return resultList;
    }
    private HashSet<ISearchable> SearchInMap(string word)
    {
        if (MyInvertedIndex.MyInvertedIndex.Map.ContainsKey(word))
        {
            return new HashSet<ISearchable>(MyInvertedIndex.MyInvertedIndex.Map[word]);
        }
        else
        {
            return new HashSet<ISearchable>();
        }
    }
}