using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;

namespace FullTextsearch.SearchManager.SignedSearchManager;

public interface ISignedSearchStrategy
{
    HashSet<ISearchable> GetResult(HashSet<string> unSignedWords, HashSet<string> positiveWords,
        HashSet<string> negativeWords, HashSet<ISearchable> unSignedResult, HashSet<ISearchable> positiveResult,
        HashSet<ISearchable> negativeResult, HashSet<ISearchable> allResults);
}