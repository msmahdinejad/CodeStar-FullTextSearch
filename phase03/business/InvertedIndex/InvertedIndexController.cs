using Microsoft.VisualBasic;

namespace phase02;
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
        else
        {
            return new HashSet<ISearchable>();
        }
    }
    public HashSet<ISearchable> GetAllValue()
    {
        var allValue = new HashSet<ISearchable>();
        foreach (var value in _map)
        {
            allValue.UnionWith(value.Value);
        }
        return allValue;
    }
}