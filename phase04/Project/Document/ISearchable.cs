namespace phase02.Document;

public interface ISearchable
{
    IEnumerable<string> GetKey();
    string GetValue();
}