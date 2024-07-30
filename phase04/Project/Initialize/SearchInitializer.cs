using phase02.Document;
using phase02.Factory.FolderFactory;
using phase02.Factory.SearchFactory;
using phase02.InvertedIndex;
using phase02.QueryModel;
using phase02.SearchManager;

namespace phase02.Initialize;

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