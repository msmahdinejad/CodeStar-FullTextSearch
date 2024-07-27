namespace phase02;

public class SearchWithSign : ISearch
{
    private HashSet<ISearchable> _unSignedResult { get; set; }
    private HashSet<ISearchable> _positiveResult { get; set; }
    private HashSet<ISearchable> _negativeResult { get; set; }
    private HashSet<ISearchable> _finalResult { get; set; }
    private InvertedIndexController _myInvertedIndex { get; init; }
    public SearchWithSign(InvertedIndexController myInvertedIndex)
    {
        _unSignedResult = new HashSet<ISearchable>();
        _positiveResult = new HashSet<ISearchable>();
        _negativeResult = new HashSet<ISearchable>();
        _finalResult = new HashSet<ISearchable>();
        _myInvertedIndex = myInvertedIndex;
    }
    private void UnSignedSearch(QueryWithSign query)
    {
        if (query.UnSignedWords.Count >= 1)
        {
            _unSignedResult = SearchInMap(query.UnSignedWords.First());
            foreach (var word in query.UnSignedWords)
            {
                _unSignedResult.IntersectWith(SearchInMap(word));
            }
        }
    }
    private void PositiveSearch(QueryWithSign query)
    {
        if (query.PositiveWords.Count >= 1)
        {
            _positiveResult = SearchInMap(query.PositiveWords.First());
            foreach (var word in query.PositiveWords)
            {
                _positiveResult.UnionWith(SearchInMap(word));
            }
        }
    }
    private void NegativeSearch(QueryWithSign query)
    {
        if (query.NegativeWords.Count >= 1)
        {
            _negativeResult = SearchInMap(query.NegativeWords.First());
            foreach (var word in query.NegativeWords)
            {
                _negativeResult.UnionWith(SearchInMap(word));
            }
        }
    }

    private HashSet<ISearchable> SearchInMap(string word)
    {
        if(_myInvertedIndex.MyInvertedIndex.Map.ContainsKey(word))
        {
            return new HashSet<ISearchable>(_myInvertedIndex.MyInvertedIndex.Map[word]);
        }
        else
        {
            return new HashSet<ISearchable>();
        }
    }
    private void HaveUnsignedStrategy(QueryWithSign query)
    {
        _unSignedResult.ExceptWith(_negativeResult);
        if (query.PositiveWords.Count != 0)
        {
            _unSignedResult.IntersectWith(_positiveResult);
        }
        _finalResult = _unSignedResult;
    }
    private void HavePositiveStrategy(QueryWithSign query)
    {
        _positiveResult.ExceptWith(_negativeResult);
        _finalResult = _positiveResult;
    }
    private void HaveNegativeStrategy(QueryWithSign query)
    {
        _finalResult = new HashSet<ISearchable>(_myInvertedIndex.AllData);
        _finalResult.ExceptWith(_negativeResult);
    }
    private void Strategy(QueryWithSign query)
    {
        UnSignedSearch(query);
        PositiveSearch(query);
        NegativeSearch(query);
        if (_unSignedResult.Count > 0)
        {
            HaveUnsignedStrategy(query);
        }
        else if (_positiveResult.Count > 0)
        {
            HavePositiveStrategy(query);
        }
        else if (_negativeResult.Count > 0)
        {
            HaveNegativeStrategy(query);
        }
    }
    public HashSet<ISearchable> SearchWithQuery(Query inputQuery)
    {
        QueryWithSign query = new QueryWithSign(inputQuery);
        query.Build();
        Strategy(query);
        return _finalResult;
    }
}