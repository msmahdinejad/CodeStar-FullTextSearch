using FullTextsearch.Document;
using FullTextsearch.Document.Extractor;

namespace FullTextsearch.InvertedIndex;

public class InvertedIndexController : IInvertedIndex
{
    private Dictionary<string, HashSet<ISearchable>> _map;

    public InvertedIndexController()
    {
        _map = new Dictionary<string, HashSet<ISearchable>>();
    }

    public void AddDataToMap(ISearchable myData, IExtractor myExtractor)
    {
        foreach (var key in myExtractor.GetKey(myData))
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

    public void AddDataListToMap(IEnumerable<ISearchable> dataList, IExtractor myExtractor)
    {
        foreach (var data in dataList)
        {
            AddDataToMap(data, myExtractor);
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