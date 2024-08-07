namespace FullTextsearch.QueryManager.WordFinder;

public class PositiveWordFinder : IWordFinder
{
    private static PositiveWordFinder _positiveWordFinder;
    public static PositiveWordFinder Instance => _positiveWordFinder ??= new PositiveWordFinder();
    private const char PositiveChar = '+';

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