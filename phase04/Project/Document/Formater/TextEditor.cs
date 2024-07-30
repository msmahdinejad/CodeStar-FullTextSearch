using System.Text.RegularExpressions;

namespace phase02.Document.Formater;
public class TextEditor
{
    private static TextEditor _textEditor;
    public static TextEditor Instance => _textEditor ??= new TextEditor();
    private readonly string regexFormat = @"[^\w\sآ-ی]";
    private readonly char[] WhitespaceCharacters = { ' ', '\t', '\n', '\r' };

    
    public string[] TextSpliter(string text)
    {
        var textUpperCase = text.ToUpper();
        text = Regex.Replace(textUpperCase, regexFormat, " ");
        string[] wordList = text.Split(WhitespaceCharacters, StringSplitOptions.RemoveEmptyEntries);
        return wordList;
    }
}