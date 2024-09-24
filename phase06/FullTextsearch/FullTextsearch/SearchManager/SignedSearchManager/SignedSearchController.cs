using FullTextsearch.Document.Abstraction;
using FullTextsearch.InvertedIndex.Abstraction;
using FullTextsearch.QueryModel.Abstraction;
using FullTextsearch.SearchManager.Abstraction;
using FullTextsearch.SearchManager.ResultList;
using FullTextsearch.SearchManager.ResultList.Abstraction;
using FullTextsearch.WordFinder;
using FullTextsearch.WordFinder.Abstraction;

namespace FullTextsearch.SearchManager.SignedSearchManager;

public class SignedSearchController : ISearchController
{
    private ISignedSearchStrategy _signedSearchStrategy;

    private IEnumerable<IWordFinder> _wordFinders;
    private Dictionary<WordFinderType, HashSet<string>> _dictionaryWordFinder;

    private IEnumerable<IResultListMaker> _resultListMakers;
    private Dictionary<ResultListMakerType, IResultListMaker> _dictionaryResultListMaker;

    public SearchStrategyType SearchStrategyName => SearchStrategyType.SignedSearch;


    public SignedSearchController(IEnumerable<IWordFinder> wordFinders, IEnumerable<IResultListMaker> resultListMakers,
        ISignedSearchStrategy signedSearchStrategy)
    {
        _signedSearchStrategy = signedSearchStrategy;
        _wordFinders = wordFinders;
        _resultListMakers = resultListMakers;

        _dictionaryWordFinder = new();
        _dictionaryResultListMaker = new();
    }

    public HashSet<ISearchable> SearchWithQuery(IQuery inputQuery, IInvertedIndex myInvertedIndex)
    {
        foreach (var wordFinder in _wordFinders)
        {
            _dictionaryWordFinder[wordFinder.Type] = wordFinder.FindWords(inputQuery.SplitedText);
        }

        foreach (var resultListMaker in _resultListMakers)
        {
            _dictionaryResultListMaker[resultListMaker.Type] = resultListMaker;
        }

        var unSignedResult = _dictionaryResultListMaker[ResultListMakerType.Intersect]
            .MakeResultList(_dictionaryWordFinder[WordFinderType.Unsigned], myInvertedIndex);
        var positiveResult = _dictionaryResultListMaker[ResultListMakerType.Union]
            .MakeResultList(_dictionaryWordFinder[WordFinderType.Positive], myInvertedIndex);
        var negativeResult = _dictionaryResultListMaker[ResultListMakerType.Union]
            .MakeResultList(_dictionaryWordFinder[WordFinderType.Negative], myInvertedIndex);
        var finalResult = _signedSearchStrategy.GetResult(_dictionaryWordFinder[WordFinderType.Unsigned],
            _dictionaryWordFinder[WordFinderType.Positive], _dictionaryWordFinder[WordFinderType.Negative],
            unSignedResult, positiveResult, negativeResult, myInvertedIndex.GetAllValue());
        return finalResult;
    }
}