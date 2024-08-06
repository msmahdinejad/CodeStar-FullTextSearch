using FullTextsearch.Document.Formater;

namespace FullTextsearch.Document;

public class Document : ISearchable
{
    public  string DocName { get; }
    public string Text { get; }
    public ITextEditor TextEditor { get; }

    public Document(string docName, string text, ITextEditor textEditor)
    {
        DocName = docName;
        Text = text;
        TextEditor = textEditor;
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
    
    public IEnumerable<string> GetWords()
    {
        return TextEditor.TextSplitter(Text);
    }

    public string GetValue()
    {
        return DocName;
    }
}