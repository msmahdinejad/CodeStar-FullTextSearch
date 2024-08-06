namespace FullTextsearch.Document.Formater;

public interface ITextEditor
{
    IEnumerable<string> TextSplitter(string text);
}