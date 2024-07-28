namespace phase02;

public class Processor : IProcessor
{
    private IEnumerable<ISearchable> _dataList;
    private InvertedIndexController _invertedIndex;
    private ISearchStrategy _searchcontroller;
    private string _folderPath;
    private string _className;
    private string _searchType;
    public Processor(string folderPath, string className, string searchType)
    => (_folderPath, _className, _searchType) = (folderPath, className, searchType);

    public void Build()
    {
        _dataList = FolderReaderFactory.Instance.ReadFolderDataList(_className, _folderPath);
        _invertedIndex = new InvertedIndexController();
        _invertedIndex.AddDataListToMap(_dataList);
        _searchcontroller = SearchStrategyFactory.Instance.makeSearchcontroller(_invertedIndex, _searchType);
    }
    public HashSet<ISearchable> Search(Query query)
    {
        return _searchcontroller.SearchWithQuery(query);
    }
}