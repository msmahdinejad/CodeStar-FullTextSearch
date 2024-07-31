using phase02.Document;

namespace phase02.SearchManager;

public interface ISignedSearchStrategy
{
    HashSet<ISearchable> GetResult(HashSet<ISearchable> unSignedResult, HashSet<ISearchable> positiveResult,
        HashSet<ISearchable> negativeResult, HashSet<ISearchable> allResults);
}