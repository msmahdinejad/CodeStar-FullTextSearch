namespace phase02;

public class Processor : IProcessor
{
    private ISearchStrategy _searchcontroller;
    private IInvertedIndex _invertedIndex;
    public void Build(string className, string folderPath, string searchType, FolderReaderFactory inputFolderReaderFactory, IInvertedIndex inputInvertedIndex, SearchStrategyFactory inputSearchStrategyFactory)
    {
        
        var dataList = inputFolderReaderFactory.ReadFolderDataList(className, folderPath);
        inputInvertedIndex.AddDataListToMap(dataList);
        _invertedIndex = inputInvertedIndex;
        _searchcontroller = inputSearchStrategyFactory.makeSearchcontroller(searchType);
    }
    public HashSet<ISearchable> Search(Query query)
    {
        return _searchcontroller.SearchWithQuery(query, _invertedIndex);
    }
}