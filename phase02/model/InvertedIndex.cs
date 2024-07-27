namespace phase02;

public class InvertedeIndex
{
    public Dictionary<string, HashSet<ISearchable>> Map { get; init; }
    public InvertedeIndex()
    {
        Map = new Dictionary<string, HashSet<ISearchable>>();
    }
}