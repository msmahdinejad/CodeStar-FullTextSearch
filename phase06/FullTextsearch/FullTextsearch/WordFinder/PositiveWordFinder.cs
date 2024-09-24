using FullTextsearch.WordFinder.Abstraction;

namespace FullTextsearch.WordFinder;

public class PositiveWordFinder : IWordFinder
{
    private readonly char PositiveChar = '+';

    public WordFinderType Type { get; init; } = WordFinderType.Positive;
        
    public HashSet<string> FindWords(IEnumerable<string> words)
    {
        var result = words
            .Where(word => word[0] == PositiveChar)
            .Select(word => word.Substring(1))
            .ToHashSet();

        return result;
    }
}