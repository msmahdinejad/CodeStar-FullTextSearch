namespace phase02;

public class Query
{
    public string Text { get; init; }
    public string[] SplitedText => Text.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    public Query(string text) => Text = text;
}