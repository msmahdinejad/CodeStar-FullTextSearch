namespace phase02;

public class SearchWithSign : ISearch
{
    public HashSet<ISearchable> UnSignedDocumentsResult { get; set; }
    public HashSet<ISearchable> PositiveDocumentsResult { get; set; }
    public HashSet<ISearchable> NegativeDocumentsResult { get; set; }
    public HashSet<ISearchable> FinalResult { get; set; }
    public InvertedIndexController MyInvertedIndex { get; init; }
    public SearchWithSign(InvertedIndexController myInvertedIndex)
    {
        UnSignedDocumentsResult = new HashSet<ISearchable>();
        PositiveDocumentsResult = new HashSet<ISearchable>();
        NegativeDocumentsResult = new HashSet<ISearchable>();
        FinalResult = new HashSet<ISearchable>();
        MyInvertedIndex = myInvertedIndex;
    }
    private void UnSignedSearch(QueryWithSign query)
    {
        if (query.UnSignedWords.Count >= 1)
        {
            UnSignedDocumentsResult = Search(query.UnSignedWords.First());
            foreach (var word in query.UnSignedWords)
            {
                UnSignedDocumentsResult.IntersectWith(Search(word));
            }
        }
    }
    private void PositiveSearch(QueryWithSign query)
    {
        if (query.PositiveWords.Count >= 1)
        {
            PositiveDocumentsResult = Search(query.PositiveWords.First());
            foreach (var word in query.PositiveWords)
            {
                PositiveDocumentsResult.UnionWith(Search(word));
            }
        }
    }
    private void NegativeSearch(QueryWithSign query)
    {
        if (query.NegativeWords.Count >= 1)
        {
            NegativeDocumentsResult = Search(query.NegativeWords.First());
            foreach (var word in query.NegativeWords)
            {
                NegativeDocumentsResult.UnionWith(Search(word));
            }
        }
    }

    private HashSet<ISearchable> Search(string word)
    {
        if(MyInvertedIndex.MyInvertedIndex.Map.ContainsKey(word))
        {
            return new HashSet<ISearchable>(MyInvertedIndex.MyInvertedIndex.Map[word]);
        }
        else
        {
            return new HashSet<ISearchable>();
        }
    }
    private void HaveUnsignedStrategy(QueryWithSign query)
    {
        UnSignedDocumentsResult.ExceptWith(NegativeDocumentsResult);
        if (query.PositiveWords.Count != 0)
        {
            UnSignedDocumentsResult.IntersectWith(PositiveDocumentsResult);
        }
        FinalResult = UnSignedDocumentsResult;
    }
    private void HavePositiveStrategy(QueryWithSign query)
    {
        PositiveDocumentsResult.ExceptWith(NegativeDocumentsResult);
        FinalResult = PositiveDocumentsResult;
    }
    private void HaveNegativeStrategy(QueryWithSign query)
    {
        FinalResult = new HashSet<ISearchable>(MyInvertedIndex.AllData);
        FinalResult.ExceptWith(NegativeDocumentsResult);
    }
    private void Strategy(QueryWithSign query)
    {
        UnSignedSearch(query);
        PositiveSearch(query);
        NegativeSearch(query);
        if (UnSignedDocumentsResult.Count > 0)
        {
            HaveUnsignedStrategy(query);
        }
        else if (PositiveDocumentsResult.Count > 0)
        {
            HavePositiveStrategy(query);
        }
        else if (NegativeDocumentsResult.Count > 0)
        {
            HaveNegativeStrategy(query);
        }
    }
    public HashSet<ISearchable> SearchWithQuery(Query q)
    {
        QueryWithSign query = new QueryWithSign(q);
        query.Build();
        Strategy(query);
        return FinalResult;
    }
}