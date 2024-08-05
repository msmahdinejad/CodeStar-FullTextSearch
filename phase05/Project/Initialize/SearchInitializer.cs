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
    ITextEditor textEditor, IExtractor extractor)
    : ISearchInitializer
{
    public ISearchController SearchController { get; set; }
    public IDataFolderReader DataFolderReader { get; set; }
    public void Build(DataType className, string folderPath, SearchStrategyType searchType)
    {

        DataFolderReader = inputDataFolderReaderFactory.MakeDataFolderReader(className);
        var dataList = DataFolderReader.ReadDataListFromFolder(folderPath, textEditor);
        invertedIndex.AddDataListToMap(dataList, extractor);
        SearchController = inputSearchStrategyFactory.MakeSearchController(searchType);
    }
    public HashSet<ISearchable> Search(IQuery query)
    {
        return SearchController.SearchWithQuery(query, invertedIndex);
    }
}