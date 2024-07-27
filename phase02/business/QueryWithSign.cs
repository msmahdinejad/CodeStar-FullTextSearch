namespace phase02;

public class QueryWithSign : IQuery
{
    public Query MyQuery { get; init; }
    public HashSet<string> UnSignedWords { get; init; }
    public HashSet<string> PositiveWords { get; init; }
    public HashSet<string> NegativeWords { get; init; }
    public QueryWithSign(Query query)
    {
        UnSignedWords = new HashSet<string>();
        PositiveWords = new HashSet<string>();
        NegativeWords = new HashSet<string>();
        MyQuery = query;
    }
    private void FindUnSignWords()
    {
        foreach (var word in MyQuery.SplitedText)
        {
            if (word[0] != '+' && word[0] != '-')
            {
                UnSignedWords.Add(word);
            }
        }
    }
    private void FindPositiveWords()
    {
        foreach (var word in MyQuery.SplitedText)
        {
            if (word[0] == '+')
            {
                PositiveWords.Add(word.Substring(1));
            }
        }
    }
    private void FindNegativeWords()
    {
        foreach (var word in MyQuery.SplitedText)
        {
            if (word[0] == '-')
            {
                NegativeWords.Add(word.Substring(1));
            }
        }
    }
    public void Build()
    {
        MyQuery.Build();
        FindUnSignWords();
        FindPositiveWords();
        FindNegativeWords();
    }
}