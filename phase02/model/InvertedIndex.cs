namespace phase02;


public class InvertedeIndex
{
    public Dictionary<string, HashSet<string>> Words { get; init; }
    public InvertedeIndex()
    {
        this.Words = new Dictionary<string, HashSet<string>>();
    }
}