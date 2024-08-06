namespace FullTextsearch.QueryManager.WordFinder;

public interface IWordFinder
{
    HashSet<string> FindWords(string[] words);
}