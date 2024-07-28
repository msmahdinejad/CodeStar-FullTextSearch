namespace phase02;

public class SignedSearchStrategy : ISearchStrategy
{
    private HashSet<ISearchable> _unSignedResult;
    private HashSet<ISearchable> _positiveResult;
    private HashSet<ISearchable> _negativeResult;
    private HashSet<ISearchable> _finalResult;
    private const string _searchStrategyName = "SignedSearch";
    public SignedSearchStrategy()
    {
        _unSignedResult = new HashSet<ISearchable>();
        _positiveResult = new HashSet<ISearchable>();
        _negativeResult = new HashSet<ISearchable>();
        _finalResult = new HashSet<ISearchable>();
    }
    public string GetSearchStrategyName()
    {
        return _searchStrategyName;
    }
    private void HaveUnsignedStrategy(SignedQuery query)
    {
        _unSignedResult.ExceptWith(_negativeResult);
        if (query.PositiveWords.Count != 0)
        {
            _unSignedResult.IntersectWith(_positiveResult);
        }
        _finalResult = _unSignedResult;
    }
    private void HavePositiveStrategy()
    {
        _positiveResult.ExceptWith(_negativeResult);
        _finalResult = _positiveResult;
    }
    private void HaveNegativeStrategy(IInvertedIndex myInvertedIndex)
    {
        _finalResult = new HashSet<ISearchable>(myInvertedIndex.GetAllValue());
        _finalResult.ExceptWith(_negativeResult);
    }
    private void Strategy(SignedQuery query, IInvertedIndex myInvertedIndex)
    {
        _unSignedResult = IntersectResultList.Instance.MakeResultList(query.UnSignedWords, myInvertedIndex);
        _positiveResult = UnionResultList.Instance.MakeResultList(query.PositiveWords, myInvertedIndex);
        _negativeResult = UnionResultList.Instance.MakeResultList(query.NegativeWords, myInvertedIndex);
        if (_unSignedResult.Count > 0)
        {
            HaveUnsignedStrategy(query);
        }
        else if (_positiveResult.Count > 0)
        {
            HavePositiveStrategy();
        }
        else if (_negativeResult.Count > 0)
        {
            HaveNegativeStrategy(myInvertedIndex);
        }
    }
    public HashSet<ISearchable> SearchWithQuery(Query inputQuery, IInvertedIndex myInvertedIndex)
    {
        SignedQuery query = new SignedQuery(inputQuery);
        query.Build();
        Strategy(query, myInvertedIndex);
        return _finalResult;
    }
}