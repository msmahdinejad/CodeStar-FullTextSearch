using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace phase02;
public class TextEditor
{
    private static TextEditor _textEditor;
    public static TextEditor Instance => _textEditor ??= new TextEditor();
    private const string regexFormat = @"[^\w\sآ-ی]";
    public string[] TextSpliter(string text)
    {
        var textUpperCase = text.ToUpper();
        text = Regex.Replace(textUpperCase, regexFormat, " ");
        string[] wordList = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return wordList;
    }
}