using FullTextsearch.Document.Abstraction;
using FullTextsearch.Document.Extractor.Abstraction;

namespace FullTextsearch.InvertedIndex.Abstraction;

public interface IInvertedIndex
{
    void AddDataToMap(ISearchable myData, IExtractor myExtractor);
    void AddDataListToMap(IEnumerable<ISearchable> dataList, IExtractor myExtractor);
    HashSet<ISearchable> GetValue(string word);
    HashSet<ISearchable> GetAllValue();
}