using Microsoft.VisualBasic;

namespace phase02;
public class InvertedIndexController
{
    public HashSet<ISearchable> AllData { get; init; }
    public InvertedeIndex MyInvertedIndex { get; init; }
    public InvertedIndexController()
    {
        AllData = new HashSet<ISearchable>();
        MyInvertedIndex = new InvertedeIndex();
    }
    public void AddDataToMap(ISearchable myData)
    {
        foreach (var key in myData.GetKey())
        {
            if (!MyInvertedIndex.Map.ContainsKey(key))
            {
                MyInvertedIndex.Map[key] = [myData];
            }
            else
            {
                MyInvertedIndex.Map[key].Add(myData);
            }
        }
        AllData.Add(myData);
    }
    public void AddListToMap(IEnumerable<ISearchable> dataList)
    {
        foreach (var data in dataList)
        {
            AddDataToMap(data);
        }
    }
}