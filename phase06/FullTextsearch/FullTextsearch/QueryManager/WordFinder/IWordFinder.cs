namespace FullTextsearch.QueryManager.WordFinder;

public interface IWordFinder
{
    WordFinderType Type { get; init; }
    HashSet<string> FindWords(IEnumerable<string> words);
}