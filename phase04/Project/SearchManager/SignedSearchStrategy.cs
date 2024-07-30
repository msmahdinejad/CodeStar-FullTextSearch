using phase02.Document;
using phase02.InvertedIndex;
using phase02.QueryManager;
using phase02.QueryModel;
using phase02.SearchManager.ResultList;

namespace phase02.SearchManager;

public class SignedSearchStrategy : ISearchStrategy
{
    private HashSet<ISearchable> _unSignedResult;
    private HashSet<ISearchable> _positiveResult;
    private HashSet<ISearchable> _negativeResult;
    private HashSet<ISearchable> _finalResult;
    public SearchStrategyType SearchStrategyName => SearchStrategyType.SignedSearch;
    

    public SignedSearchStrategy()
    {
        _unSignedResult = new HashSet<ISearchable>();
        _positiveResult = new HashSet<ISearchable>();
        _negativeResult = new HashSet<ISearchable>();
        _finalResult = new HashSet<ISearchable>();
    }

    public string GetSearchStrategyName()
    {
        return SearchStrategyName.ToString();
    }

    private void HaveUnsignedStrategy(SignedQueryBuilder queryBuilder)
    {
        _unSignedResult.ExceptWith(_negativeResult);
        if (queryBuilder.PositiveWords.Count != 0)
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

    private void Strategy(SignedQueryBuilder queryBuilder, IInvertedIndex myInvertedIndex)
    {
        _unSignedResult = IntersectResultListMaker.Instance.MakeResultList(queryBuilder.UnSignedWords, myInvertedIndex);
        _positiveResult = UnionResultListMaker.Instance.MakeResultList(queryBuilder.PositiveWords, myInvertedIndex);
        _negativeResult = UnionResultListMaker.Instance.MakeResultList(queryBuilder.NegativeWords, myInvertedIndex);
        if (_unSignedResult.Count > 0)
        {
            HaveUnsignedStrategy(queryBuilder);
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
        SignedQueryBuilder queryBuilder = new SignedQueryBuilder();
        queryBuilder.Build(inputQuery);
        Strategy(queryBuilder, myInvertedIndex);
        return _finalResult;
    }
}