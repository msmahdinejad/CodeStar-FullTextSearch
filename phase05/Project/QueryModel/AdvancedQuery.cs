using System.Text.RegularExpressions;

namespace phase02.QueryModel;

public class AdvancedQuery : IQuery
{
    public string Text { get; init; }
    public string[] SplitedText => AdvancedSplitter(Text);
    public AdvancedQuery(string text) => Text = text;
    
    private List<string> ExtractSingleWord(string searchInput) 
    {
        string singleWords = Regex.Replace(searchInput, "([+-| ]\"([^\"]*)\")", "");
        var splitInput = singleWords.Split(" ").ToList();
        List<string> result = new List<string>();
        foreach (var word in splitInput)
        {
            if (word != "")
            {
                result.Add(word.ToString());
            }
        }

        return result;
    }

    private List<string> ExtractPhrase(string searchInput)
    { 
        Regex sign = new Regex(@"([+-]?)\s*""([^""]*)""");
        Regex phease = new Regex(@"[-+ ]""([^""]*)""");
        List<string> phrases = phease.Matches(searchInput)
            .Cast<Match>()
            .Select(match => match.Groups[1].Value)
            .ToList();
        List<string> signs = sign.Matches(searchInput)
            .Cast<Match>()
            .Select(match => match.Groups[1].Value)
            .ToList();
        List<string> result = signs.Zip(phrases, (sign, phrase) => sign + phrase).ToList();
        return result;
    }

    private string[] AdvancedSplitter(string inputSearch)
    {
        return ExtractSingleWord(inputSearch).Concat(ExtractPhrase(inputSearch)).ToArray();
    }
}