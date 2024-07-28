namespace phase02;

public interface IInitializer
{
    void Build(string className, string folderPath, string searchType, DataFolderReaderFactory inputDataFolderReaderFactory, IInvertedIndex inputInvertedIndex, SearchStrategyFactory inputSearchStrategyFactory);
    HashSet<ISearchable> Search(Query query);
}