using System.Text.RegularExpressions;

namespace phase02.QueryModel;

public class AdvancedQuery : IQuery
{
    public string Text { get; init; }
    public string[] SplitedText => AdvancedSplitter(Text.ToUpper());
    public AdvancedQuery(string text) => Text = text;

    private List<string> ExtractSingleWord(string text)
    {
        var singleWords = Regex.Replace(text, "([+-| ]\"([^\"]*)\")", "");
        var splitInput = singleWords.Split(" ").ToList();
        var result = new List<string>();
        foreach (var word in splitInput)
        {
            if (word != "")
            {
                result.Add(word.ToString());
            }
        }

        return result;
    }

    private List<string> ExtractPhrase(string text)
    {
        var sign = new Regex(@"([+-]?)\s*""([^""]*)""");
        var phease = new Regex(@"[-+ ]""([^""]*)""");
        var phrases = phease.Matches(text)
            .Cast<Match>()
            .Select(match => match.Groups[1].Value)
            .ToList();
        var signs = sign.Matches(text)
            .Cast<Match>()
            .Select(match => match.Groups[1].Value)
            .ToList();
        var result = signs.Zip(phrases, (sign, phrase) => sign + phrase).ToList();
        return result;
    }

    private string[] AdvancedSplitter(string inputSearch)
    {
        return ExtractSingleWord(inputSearch).Concat(ExtractPhrase(inputSearch)).ToArray();
    }
}