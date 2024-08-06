namespace FullTextsearch.QueryModel;

public interface IQuery
{
    string Text { get; init; }
    string[] SplitedText { get; }
}