namespace FullTextsearch.WordFinder.Abstraction;

public interface IWordFinder
{
    WordFinderType Type { get; init; }
    HashSet<string> FindWords(IEnumerable<string> words);
}