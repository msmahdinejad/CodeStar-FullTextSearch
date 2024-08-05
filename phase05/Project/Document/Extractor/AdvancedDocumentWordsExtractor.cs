namespace phase02.Document.Extractor;

public class AdvancedDocumentWordsExtractor : IExtractor
{
    public IEnumerable<string> GetKey(ISearchable data)
    {
        return data.GetWords();
    }
}