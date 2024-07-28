namespace phase02;

public class Initializer : IInitializer
{
    private ISearchStrategy _searchcontroller;
    private IInvertedIndex _invertedIndex;
    public void Build(string className, string folderPath, string searchType, DataFolderReaderFactory inputDataFolderReaderFactory, IInvertedIndex inputInvertedIndex, SearchStrategyFactory inputSearchStrategyFactory)
    {
        
        var dataList = inputDataFolderReaderFactory.ReadDataListFromFolder(className).ReadDataListFromFolder(folderPath);
        inputInvertedIndex.AddDataListToMap(dataList);
        _invertedIndex = inputInvertedIndex;
        _searchcontroller = inputSearchStrategyFactory.MakeSearchController(searchType);
    }
    public HashSet<ISearchable> Search(Query query)
    {
        return _searchcontroller.SearchWithQuery(query, _invertedIndex);
    }
}