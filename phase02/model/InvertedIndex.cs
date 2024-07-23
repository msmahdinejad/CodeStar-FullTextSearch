namespace phase02;


public class InvertedeIndex
{
    public Dictionary<string, HashSet<string>> Words { get; init; }
    public InvertedeIndex()
    {
        Words = new Dictionary<string, HashSet<string>>();
    }
}