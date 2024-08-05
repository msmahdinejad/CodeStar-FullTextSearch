using phase02.Document;

namespace phase02.InvertedIndex;

public class InvertedIndexController : IInvertedIndex
{
    private Dictionary<string, HashSet<ISearchable>> _map;

    public InvertedIndexController()
    {
        _map = new Dictionary<string, HashSet<ISearchable>>();
    }

    public void AddDataToMap(ISearchable myData)
    {
        foreach (var key in myData.GetKey())
        {
            if (!_map.ContainsKey(key))
            {
                _map[key] = [myData];
            }
            else
            {
                _map[key].Add(myData);
            }
        }
    }

    public void AddDataListToMap(IEnumerable<ISearchable> dataList)
    {
        foreach (var data in dataList)
        {
            AddDataToMap(data);
        }
    }

    public HashSet<ISearchable> GetValue(string word)
    {
        if (_map.ContainsKey(word))
        {
            return new HashSet<ISearchable>(_map[word]);
        }

        return new HashSet<ISearchable>();
    }

    public HashSet<ISearchable> GetAllValue()
    {
        var allValue = _map
            .SelectMany(value => value.Value)
            .ToHashSet();

        return allValue;
    }
}