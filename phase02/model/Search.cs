
namespace phase02;

public class Search
{
    public HashSet<string> UnSignedWords { get; init; }
    public HashSet<string> PositiveWords { get; init; }
    public HashSet<string> NegativeWords { get; init; }
    public Search(HashSet<string> unSignedWords, HashSet<string> positiveWords, HashSet<string> negativeWords)
    => (UnSignedWords, PositiveWords, NegativeWords) = (unSignedWords, positiveWords, negativeWords);
}