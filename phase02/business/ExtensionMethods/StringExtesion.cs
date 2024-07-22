using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace phase02;
public static class StringExtension
{
    public static string[] TextSpliter(this string text)
    {
        var textUpperCase = text.ToUpper();
        text = Regex.Replace(textUpperCase, @"[^\w\sآ-ی]", " ");
        string[] wordList = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return wordList;
    }
}