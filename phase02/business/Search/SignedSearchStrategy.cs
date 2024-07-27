namespace phase02;

public class SignedSearchStrategy : ISearch
{
    private HashSet<ISearchable> _finalResult { get; set; }
    private SignedSearch _mySignedSearch { get; init; }

    public SignedSearchStrategy(InvertedIndexController myInvertedIndex)
    {
        _mySignedSearch = new SignedSearch(myInvertedIndex);
        _finalResult = new HashSet<ISearchable>();
    }

    private void HaveUnsignedStrategy(SignedQuery query)
    {
        _mySignedSearch.UnSignedResult.ExceptWith(_mySignedSearch.NegativeResult);
        if (query.PositiveWords.Count != 0)
        {
            _mySignedSearch.UnSignedResult.IntersectWith(_mySignedSearch.PositiveResult);
        }
        _finalResult = _mySignedSearch.UnSignedResult;
    }
    private void HavePositiveStrategy(SignedQuery query)
    {
        _mySignedSearch.PositiveResult.ExceptWith(_mySignedSearch.NegativeResult);
        _finalResult = _mySignedSearch.PositiveResult;
    }
    private void HaveNegativeStrategy(SignedQuery query)
    {
        _finalResult = new HashSet<ISearchable>(_mySignedSearch.MyInvertedIndex.AllData);
        _finalResult.ExceptWith(_mySignedSearch.NegativeResult);
    }
    private void Strategy(SignedQuery query)
    {
        _mySignedSearch.UnSignedResult = _mySignedSearch.MakeResultList(query.UnSignedWords);
        _mySignedSearch.PositiveResult = _mySignedSearch.MakeResultList(query.PositiveWords);
        _mySignedSearch.NegativeResult = _mySignedSearch.MakeResultList(query.NegativeWords);
        if (_mySignedSearch.UnSignedResult.Count > 0)
        {
            HaveUnsignedStrategy(query);
        }
        else if (_mySignedSearch.PositiveResult.Count > 0)
        {
            HavePositiveStrategy(query);
        }
        else if (_mySignedSearch.NegativeResult.Count > 0)
        {
            HaveNegativeStrategy(query);
        }
    }
    public HashSet<ISearchable> SearchWithQuery(Query inputQuery)
    {
        SignedQuery query = new SignedQuery(inputQuery);
        query.Build();
        Strategy(query);
        return _finalResult;
    }
}