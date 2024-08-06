namespace FullTextsearch.QueryManager.WordFinder;

public class NegativeWordFinder : IWordFinder
{
    private static NegativeWordFinder _negativeWordFinder;
    public static NegativeWordFinder Instance => _negativeWordFinder ??= new NegativeWordFinder();
    private const char NegativeChar = '-';

    public HashSet<string> FindWords(string[] words)
    {
        var result = words
            .Where(word => word[0] == NegativeChar)
            .Select(word => word.Substring(1))
            .ToHashSet();

        return result;
    }
}