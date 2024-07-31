using phase02.Document;
using phase02.Document.Formater;
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
    ITextEditor textEditor)
    : ISearchInitializer
{
    private ISearchController _searchController;
    public void Build(DataType className, string folderPath, SearchStrategyType searchType)
    {
        
        var dataList = inputDataFolderReaderFactory.MakeDataFolderReader(className).ReadDataListFromFolder(folderPath, textEditor);
        invertedIndex.AddDataListToMap(dataList);
        _searchController = inputSearchStrategyFactory.MakeSearchController(searchType);
    }
    public HashSet<ISearchable> Search(IQuery query)
    {
        return _searchController.SearchWithQuery(query, invertedIndex);
    }
}