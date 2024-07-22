namespace phase02;


public class InvertedeIndex
{
    public Dictionary<string, HashSet<string>> Words { get; set; }
    public InvertedeIndex()
    {
        this.Words = new Dictionary<string, HashSet<string>>();
    }
}