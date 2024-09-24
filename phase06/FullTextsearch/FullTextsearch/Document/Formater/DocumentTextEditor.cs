using System.Text.RegularExpressions;
using FullTextsearch.Document.Formater.Abstraction;

namespace FullTextsearch.Document.Formater;
public class DocumentTextEditor : ITextEditor
{
    private readonly string regexFormat = @"[^\w\sآ-ی]";
    private readonly char[] WhitespaceCharacters = { ' ', '\t', '\n', '\r' };
    
    public IEnumerable<string> TextSplitter(string text)
    {
        var textUpperCase = text.ToUpper();
        text = Regex.Replace(textUpperCase, regexFormat, " ");
        string[] wordList = text.Split(WhitespaceCharacters, StringSplitOptions.RemoveEmptyEntries);
        return wordList;
    }
}