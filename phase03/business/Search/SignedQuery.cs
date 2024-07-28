namespace phase02;

public class SignedQuery : IQuery
{
    private Query _myQuery { get; init; }
    public HashSet<string> UnSignedWords { get; init; }
    public HashSet<string> PositiveWords { get; init; }
    public HashSet<string> NegativeWords { get; init; }
    public SignedQuery(Query query)
    {
        UnSignedWords = new HashSet<string>();
        PositiveWords = new HashSet<string>();
        NegativeWords = new HashSet<string>();
        _myQuery = query;
    }
    private void FindUnSignWords()
    {
        foreach (var word in _myQuery.SplitedText)
        {
            if (word[0] != '+' && word[0] != '-')
            {
                UnSignedWords.Add(word);
            }
        }
    }
    private void FindPositiveWords()
    {
        foreach (var word in _myQuery.SplitedText)
        {
            if (word[0] == '+')
            {
                PositiveWords.Add(word.Substring(1));
            }
        }
    }
    private void FindNegativeWords()
    {
        foreach (var word in _myQuery.SplitedText)
        {
            if (word[0] == '-')
            {
                NegativeWords.Add(word.Substring(1));
            }
        }
    }
    public void Build()
    {
        _myQuery.Build();
        FindUnSignWords();
        FindPositiveWords();
        FindNegativeWords();
    }
}