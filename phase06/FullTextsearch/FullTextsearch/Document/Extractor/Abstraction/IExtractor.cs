using FullTextsearch.Document.Abstraction;

namespace FullTextsearch.Document.Extractor.Abstraction;

public interface IExtractor
{
    IEnumerable<string> GetKey(ISearchable data);
}