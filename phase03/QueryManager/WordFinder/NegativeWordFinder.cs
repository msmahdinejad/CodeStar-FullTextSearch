namespace phase02;

public class NegativeWordFinder : IWordFinder
{
    private static NegativeWordFinder _negativeWordFinder;
    public static NegativeWordFinder Instance => _negativeWordFinder ??= new NegativeWordFinder();

    public HashSet<string> FindWords(Query query)
    {
        var result = query.SplitedText
            .Where(word => word[0] == '-')
            .Select(word => word.Substring(1))
            .ToHashSet();

        return result;
    }
}