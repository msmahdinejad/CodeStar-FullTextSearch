using phase02.Document.Formater;

namespace phase02.Document;

public class Document(string docName, string text, ITextEditor _textEditor) : ISearchable
{
    public IEnumerable<string> GetKey()
    {
        return _textEditor.TextSplitter(text);
    }

    public string GetValue()
    {
        return docName;
    }
}