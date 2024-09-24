namespace FullTextsearch.Document.Formater.Abstraction;

public interface ITextEditor
{
    IEnumerable<string> TextSplitter(string text);
}