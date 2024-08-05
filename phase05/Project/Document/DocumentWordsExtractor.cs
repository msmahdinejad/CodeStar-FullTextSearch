using phase02.Document.Formater;

namespace phase02.Document;

public class DocumentWordsExtractor : IExtractor
{
    public IEnumerable<string> GetKey(ISearchable data)
    {
        return data.GetWords();
    }
}