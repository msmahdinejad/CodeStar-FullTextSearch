namespace phase02;

public class InvertedIndex
{
    public Dictionary<string, HashSet<ISearchable>> Map { get; init; }
    public InvertedIndex()
    {
        Map = new Dictionary<string, HashSet<ISearchable>>();
    }
}