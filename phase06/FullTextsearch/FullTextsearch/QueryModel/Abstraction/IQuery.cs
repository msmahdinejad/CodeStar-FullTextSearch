namespace FullTextsearch.QueryModel.Abstraction;

public interface IQuery
{
    string Text { get; init; }
    string[] SplitedText { get; }
}