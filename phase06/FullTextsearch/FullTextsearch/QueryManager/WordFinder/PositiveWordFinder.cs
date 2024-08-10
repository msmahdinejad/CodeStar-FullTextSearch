namespace FullTextsearch.QueryManager.WordFinder;

public class PositiveWordFinder : IWordFinder
{
    private readonly char PositiveChar = '+';

    public WordFinderType Type { get; init; } = WordFinderType.Positive;
        
    public HashSet<string> FindWords(string[] words)
    {
        var result = words
            .Where(word => word[0] == PositiveChar)
            .Select(word => word.Substring(1))
            .ToHashSet();

        return result;
    }
}