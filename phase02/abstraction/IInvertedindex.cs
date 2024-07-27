namespace phase02;
public interface IInvertedIndex
{
    void AddDataToMap(ISearchable myData);
    void AddDataListToMap(IEnumerable<ISearchable> dataList);
}