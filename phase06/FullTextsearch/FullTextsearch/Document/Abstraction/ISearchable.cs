namespace FullTextsearch.Document.Abstraction;

public interface ISearchable
{
    IEnumerable<string> GetWords();
    string GetValue();
}