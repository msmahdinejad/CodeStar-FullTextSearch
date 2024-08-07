using FullTextsearch.Document;

namespace FullTextsearch.DbValue;

public class DbValue(string name) : ISearchable
{
    
    public IEnumerable<string> GetWords()
    {
        return new List<string>();
    }

    public string GetValue()
    {
        return name;
    }
}