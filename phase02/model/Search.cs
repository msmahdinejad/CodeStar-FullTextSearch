
namespace phase02;

public class Search
{
    public HashSet<string> UnSignedWords {get; set;}
    public HashSet<string> PositiveWords {get; set;}
    public HashSet<string> NegativeWords {get; set;}
    public Search(HashSet<string> UnSignedWords, HashSet<string> PositiveWords, HashSet<string> NegativeWords)
    {
        this.UnSignedWords = UnSignedWords;
        this.PositiveWords = PositiveWords;
        this.NegativeWords = NegativeWords;
    }
}