namespace phase02;
public interface IInvertedIndex
{
    void AddDataToMap(ISearchable myData);
    void AddDataListToMap(IEnumerable<ISearchable> dataList);
    HashSet<ISearchable> GetValue(string word);
    HashSet<ISearchable> GetAllValue();
}