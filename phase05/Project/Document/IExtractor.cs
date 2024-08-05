namespace phase02.Document;

public interface IExtractor
{
    IEnumerable<string> GetKey(ISearchable data);
}