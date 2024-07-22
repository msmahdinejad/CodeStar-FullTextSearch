using System.Diagnostics;
using System.Text;
using System.Text.RegularExpressions;

namespace phase02;
public static class StringExtension
{
    public static string[] textConverter(this string text)
    {
        text.ToUpper();
        text = Regex.Replace(text, @"[^\w\sآ-ی]", "");
        string[] wordList = text.Split(new[] { ' ', '\t', '\n', '\r' }, StringSplitOptions.RemoveEmptyEntries);
        return wordList;
    }
}