using phase02.Document;

namespace phase02.InvertedIndex;

public interface IInvertedIndex
{
    void AddDataToMap(ISearchable myData, IExtractor myExtractor);
    void AddDataListToMap(IEnumerable<ISearchable> dataList, IExtractor myExtractor);
    HashSet<ISearchable> GetValue(string word);
    HashSet<ISearchable> GetAllValue();
}