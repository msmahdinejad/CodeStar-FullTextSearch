using FullTextsearch.Document;
using FullTextsearch.Document.Extractor;

namespace FullTextsearch.InvertedIndex;

public interface IInvertedIndex
{
    void AddDataToMap(ISearchable myData, IExtractor myExtractor);
    void AddDataListToMap(IEnumerable<ISearchable> dataList, IExtractor myExtractor);
    HashSet<ISearchable> GetValue(string word);
    HashSet<ISearchable> GetAllValue();
}