using phase02.Document;
using phase02.InvertedIndex;
using phase02.QueryManager;
using phase02.QueryManager.WordFinder;
using phase02.QueryModel;
using phase02.SearchManager.ResultList;

namespace phase02.SearchManager;

public class SignedSearchController : ISearchController
{
    private IWordFinder _negativeWordFinder;
    private IWordFinder _positiveWordFinder;
    private IWordFinder _unsignedWordFinder;

    private IResultListMaker _intersectResultListMaker;
    private IResultListMaker _unionResultListMaker;

    private ISignedSearchStrategy _signedSearchStrategy;

    public SearchStrategyType SearchStrategyName => SearchStrategyType.SignedSearch;


    public SignedSearchController(IWordFinder negativeWordFinder, IWordFinder positiveWordFinder,
        IWordFinder unsignedWordFinder, IResultListMaker intersectResultListMaker,
        IResultListMaker unionResultListMaker, ISignedSearchStrategy signedSearchStrategy)
    {
        _negativeWordFinder = negativeWordFinder;
        _positiveWordFinder = positiveWordFinder;
        _unsignedWordFinder = unsignedWordFinder;
        _intersectResultListMaker = intersectResultListMaker;
        _unionResultListMaker = unionResultListMaker;
        _signedSearchStrategy = signedSearchStrategy;
    }

    public HashSet<ISearchable> SearchWithQuery(IQuery inputQuery, IInvertedIndex myInvertedIndex)
    {
        var unSignedWords = _unsignedWordFinder.FindWords(inputQuery.SplitedText);
        var positiveWords = _positiveWordFinder.FindWords(inputQuery.SplitedText);
        var negativeWords = _negativeWordFinder.FindWords(inputQuery.SplitedText);
        var unSignedResult = _intersectResultListMaker.MakeResultList(unSignedWords, myInvertedIndex);
        var positiveResult = _unionResultListMaker.MakeResultList(positiveWords, myInvertedIndex);
        var negativeResult = _unionResultListMaker.MakeResultList(negativeWords, myInvertedIndex);
        var finalResult = _signedSearchStrategy.GetResult(unSignedResult, positiveResult, negativeResult, myInvertedIndex.GetAllValue());
        return finalResult;
    }
}