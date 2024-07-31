using phase02.Document.Formater;

namespace phase02.Document;

public class Document : ISearchable
{
    public  string DocName { get; }
    public string Text { get; }
    public ITextEditor TextEditor1 { get; }

    public Document(string docName, string text, ITextEditor textEditor)
    {
        DocName = docName;
        Text = text;
        TextEditor1 = textEditor;
    }
    
    public override bool Equals(object obj)
    {
        if (obj == null || GetType() != obj.GetType())
        {
            return false;
        }
        
        var other = (Document)obj;
        
        return DocName == other.DocName && Text == other.Text;
    }
    
    public IEnumerable<string> GetKey()
    {
        return TextEditor1.TextSplitter(Text);
    }

    public string GetValue()
    {
        return DocName;
    }
}