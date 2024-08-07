using FullTextsearch.Context;
using FullTextsearch.Document;
using FullTextsearch.Document.Extractor;
using FullTextsearch.Model;

namespace FullTextsearch.InvertedIndex;

public class InvertedIndexDbController :IInvertedIndex
{
    private ApplicationDbContext _applicationDbContext=new();
    
    public void AddDataToMap(ISearchable myData, IExtractor myExtractor)
    {
        var invertedIndexMap = _applicationDbContext.InvertedIndexMap.ToList();
        foreach (var key in myExtractor.GetKey(myData))
        {
            var record = invertedIndexMap.SingleOrDefault(x => x.Key == key);
            if (record != null)
            {
                var tmp = record.Values.ToList();
                tmp.Add(myData.GetValue());
                record.Values = tmp.ToArray();
            }
            else
            {
                var newRecord = new InvertedIndexRecord(){Key = key, Values = [myData.GetValue()] };
                invertedIndexMap.Add(newRecord);
            }
        }

        _applicationDbContext.SaveChanges();
    }

    public void AddDataListToMap(IEnumerable<ISearchable> dataList, IExtractor myExtractor)
    {
        foreach (var data in dataList)
        {
            AddDataToMap(data, myExtractor);
        }    }

    public HashSet<ISearchable> GetValue(string word)
    {
        var invertedIndexMap = _applicationDbContext.InvertedIndexMap.ToList();
        var record = invertedIndexMap.SingleOrDefault(x => x.Key == word);
        if (record!= null)
        {
            return new HashSet<ISearchable>(record.Values.ToList().Select(x => new DbValue.DbValue(x)));
        }

        return new HashSet<ISearchable>();
    }

    public HashSet<ISearchable> GetAllValue()
    {
        var invertedIndexMap = _applicationDbContext.InvertedIndexMap.ToList();
        var allValue = invertedIndexMap.Select(x => x.Values.ToList().Select(y => new DbValue.DbValue(y)));
        var result = new HashSet<ISearchable>();
        foreach (var dbValues in allValue)
        {
            result.UnionWith(dbValues);
        }
        return result;
    }
}