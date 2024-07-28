namespace phase02;

public class SignedSearchStrategy : ISearchStrategy
{
    private HashSet<ISearchable> _unSignedResult;
    private HashSet<ISearchable> _positiveResult;
    private HashSet<ISearchable> _negativeResult;
    private HashSet<ISearchable> _finalResult;
    private InvertedIndexController _myInvertedIndex;
    public SignedSearchStrategy(InvertedIndexController myInvertedIndex)
    {
        _unSignedResult = new HashSet<ISearchable>();
        _positiveResult = new HashSet<ISearchable>();
        _negativeResult = new HashSet<ISearchable>();
        _myInvertedIndex = myInvertedIndex;
        _finalResult = new HashSet<ISearchable>();
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
    private void HaveNegativeStrategy()
    {
        _finalResult = new HashSet<ISearchable>(_myInvertedIndex.AllData);
        _finalResult.ExceptWith(_negativeResult);
    }
    private void Strategy(SignedQuery query)
    {
        _unSignedResult = IntersectResultList.Instance.MakeResultList(query.UnSignedWords, _myInvertedIndex);
        _positiveResult = UnionResultList.Instance.MakeResultList(query.PositiveWords, _myInvertedIndex);
        _negativeResult = UnionResultList.Instance.MakeResultList(query.NegativeWords, _myInvertedIndex);
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
            HaveNegativeStrategy();
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