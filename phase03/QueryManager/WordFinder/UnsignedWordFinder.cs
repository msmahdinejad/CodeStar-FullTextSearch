namespace phase02;

public class UnsignedWordFinder : IWordFinder
{
    private static UnsignedWordFinder _unsignedWordFinder;
    public static UnsignedWordFinder Instance => _unsignedWordFinder ??= new UnsignedWordFinder();

    public HashSet<string> FindWords(Query query)
    {
        var result = query.SplitedText
            .Where(word => word[0] != '+' && word[0] != '-')
            .ToHashSet();

        return result;
    }
}