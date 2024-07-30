using phase02.QueryModel;

namespace phase02.QueryManager.WordFinder;

public class PositiveWordFinder : IWordFinder
{
    private static PositiveWordFinder _positiveWordFinder;
    public static PositiveWordFinder Instance => _positiveWordFinder ??= new PositiveWordFinder();
    private const char PositiveChar = '+';


    public HashSet<string> FindWords(Query query)
    {
        var result = query.SplitedText
            .Where(word => word[0] == PositiveChar)
            .Select(word => word.Substring(1))
            .ToHashSet();

        return result;
    }
}