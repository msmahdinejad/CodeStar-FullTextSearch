using phase02.QueryManager.WordFinder;
using phase02.QueryModel;

namespace phase02.QueryManager;

public class SignedQueryBuilder : IQueryBuilder
{
    public HashSet<string> UnSignedWords { get; set; }
    public HashSet<string> PositiveWords { get; set; }
    public HashSet<string> NegativeWords { get; set; }

    public SignedQueryBuilder()
    {
        UnSignedWords = new HashSet<string>();
        PositiveWords = new HashSet<string>();
        NegativeWords = new HashSet<string>();
    }

    public void Build(IQuery query)
    {
        UnSignedWords = UnsignedWordFinder.Instance.FindWords(query.SplitedText);
        PositiveWords = PositiveWordFinder.Instance.FindWords(query.SplitedText);
        NegativeWords = NegativeWordFinder.Instance.FindWords(query.SplitedText);
    }
}