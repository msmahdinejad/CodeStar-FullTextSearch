namespace phase02;
public class Document : ISearchable
{
    private string _text;
    private string _docName;
    public Document(string docName, string text) => (_text, _docName) = (text, docName);
    public IEnumerable<string> GetKey()
    {
        return _text.TextSpliter();
    }
    public string GetValue()
    {
        return _docName;
    }
}