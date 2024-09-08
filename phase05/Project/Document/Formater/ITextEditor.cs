namespace phase02.Document.Formater;

public interface ITextEditor
{
    IEnumerable<string> TextSplitter(string text);
}