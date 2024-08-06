using System.Text.RegularExpressions;

namespace phase02.QueryModel;

public class AdvancedQuery : IQuery
{
    public string Text { get; init; }
    public string[] SplitedText => AdvancedSplitter(Text);
    public AdvancedQuery(string text) => Text = text;

    private string[] AdvancedSplitter(string text)
    {
        var words = text.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
        var sentenceReader = false;
        var sentence = "";
        var result = new List<string>();
        foreach (var word in words)
        {
            if (word.StartsWith('"') || word[1] == '"')
            {
                sentenceReader = true;
                sentence = word;
            }
            else if (sentenceReader)
            {
                sentence = sentence + " " + word;
            }
            else
            {
                result.Add(word);
                continue;
            }

            if (!word.EndsWith('"')) continue;
            sentenceReader = false;
            result.Add(sentence.Replace("\"", ""));
            sentence = "";

        }

        return result.ToArray();
    }
}