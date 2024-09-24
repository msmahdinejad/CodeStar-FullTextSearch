using FullTextsearch.Document.Abstraction;
using FullTextsearch.Document.Extractor.Abstraction;

namespace FullTextsearch.Document.Extractor;

public class DocumentWordsExtractor : IExtractor
{
    public IEnumerable<string> GetKey(ISearchable data)
    {
        return data.GetWords();
    }
}