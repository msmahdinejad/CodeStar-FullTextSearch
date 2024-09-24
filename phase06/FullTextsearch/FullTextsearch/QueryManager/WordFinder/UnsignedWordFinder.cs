namespace FullTextsearch.QueryManager.WordFinder;

public class UnsignedWordFinder : IWordFinder
{
    private readonly char PositiveChar = '+';

    private readonly char NegativeChar = '-';

    public WordFinderType Type { get; init; } = WordFinderType.Unsigned;
    
    public HashSet<string> FindWords(IEnumerable<string> words)
    {
        var result = words
            .Where(word => word[0] != PositiveChar && word[0] != NegativeChar)
            .ToHashSet();

        return result;
    }
}