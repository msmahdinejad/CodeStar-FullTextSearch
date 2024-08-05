namespace phase02.QueryModel;

public class AdvancedQuery : IQuery
{
    public string Text { get; init; }
    public string[] SplitedText => Text.ToUpper().Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries);
    public AdvancedQuery(string text) => Text = text;
    
}