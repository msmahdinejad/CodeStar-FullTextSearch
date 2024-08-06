namespace FullTextsearch.Document;

public interface ISearchable
{
    IEnumerable<string> GetWords();
    string GetValue();
}