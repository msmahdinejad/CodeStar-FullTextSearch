namespace phase02;

public class UnsignedWordFinder : IWordFinder
{
    private static UnsignedWordFinder _unsignedWordFinder;
    public static UnsignedWordFinder Instance => _unsignedWordFinder ??= new UnsignedWordFinder();
    
    private const char PositiveChar = '+';

    private const char NegativeChar = '-';


    public HashSet<string> FindWords(Query query)
    {
        var result = query.SplitedText
            .Where(word => word[0] != PositiveChar && word[0] != NegativeChar)
            .ToHashSet();

        return result;
    }
}