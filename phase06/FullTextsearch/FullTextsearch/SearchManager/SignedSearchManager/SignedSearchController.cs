using FullTextsearch.Document;
using FullTextsearch.InvertedIndex;
using FullTextsearch.QueryManager.WordFinder;
using FullTextsearch.QueryModel;
using FullTextsearch.SearchManager.ResultList;

namespace FullTextsearch.SearchManager.SignedSearchManager;

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
        var finalResult = _signedSearchStrategy.GetResult(unSignedWords, positiveWords, negativeWords, unSignedResult, positiveResult, negativeResult, myInvertedIndex.GetAllValue());
        return finalResult;
    }
}