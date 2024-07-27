namespace phase02;

public class Query
{
    public string Text { get; init; }
    public string[] SplitedText { get; set; }
    public Query(string text) => Text = text;
    public void Build()
    {
        var newText = Text.ToUpper();
        SplitedText = newText.QuerySpliter();
    }
}