namespace FullTextsearch.Document.Extractor;

public interface IExtractor
{
    IEnumerable<string> GetKey(ISearchable data);
}