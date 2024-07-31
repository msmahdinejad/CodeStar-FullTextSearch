using phase02.Document;

namespace phase02.SearchManager;

public class SignedSearchStrategy : ISignedSearchStrategy
{
    public HashSet<ISearchable> GetResult(HashSet<ISearchable> unSignedResult, HashSet<ISearchable> positiveResult, HashSet<ISearchable> negativeResult, HashSet<ISearchable> allResults)
    {
        HashSet<ISearchable>? finalResult;
        
        if (unSignedResult.Count > 0)
        {
            unSignedResult.ExceptWith(negativeResult);
            if (positiveResult.Count != 0)
            {
                unSignedResult.IntersectWith(positiveResult);
            }

            finalResult = new HashSet<ISearchable>(unSignedResult);
            return finalResult;
        }
        else if (positiveResult.Count > 0)
        {
            positiveResult.ExceptWith(negativeResult);
            finalResult = new HashSet<ISearchable>(positiveResult);
            return finalResult;
        }
        else if (negativeResult.Count > 0)
        {
            finalResult = new HashSet<ISearchable>(allResults);
            finalResult.ExceptWith(negativeResult);
            return finalResult;
        }

        finalResult = new HashSet<ISearchable>();
        return finalResult;
    }
}