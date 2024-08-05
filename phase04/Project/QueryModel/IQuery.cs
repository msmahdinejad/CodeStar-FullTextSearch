namespace phase02.QueryModel;

public interface IQuery
{
    string Text { get; init; }
    string[] SplitedText { get; }
}