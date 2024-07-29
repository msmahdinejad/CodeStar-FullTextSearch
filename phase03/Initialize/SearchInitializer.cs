namespace phase02;

public class SearchInitializer(
    IDataFolderReaderFactory inputDataFolderReaderFactory,
    IInvertedIndex invertedIndex,
    ISearchStrategyFactory inputSearchStrategyFactory,
    ISearchStrategy searchController)
    : ISearchInitializer
{
    public void Build(DataType className, string folderPath, SearchStrategyType searchType)
    {
        
        var dataList = inputDataFolderReaderFactory.ReadDataListFromFolder(className).ReadDataListFromFolder(folderPath);
        invertedIndex.AddDataListToMap(dataList);
        searchController = inputSearchStrategyFactory.MakeSearchController(searchType);
    }
    public HashSet<ISearchable> Search(Query query)
    {
        return searchController.SearchWithQuery(query, invertedIndex);
    }
}