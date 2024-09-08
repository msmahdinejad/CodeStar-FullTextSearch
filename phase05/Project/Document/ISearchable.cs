namespace phase02.Document;

public interface ISearchable
{
    IEnumerable<string> GetWords();
    string GetValue();
}