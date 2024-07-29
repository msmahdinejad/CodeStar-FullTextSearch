namespace phase02;

public interface IInitializer
{
    void Build(string className, string folderPath, string searchType, IDataFolderReaderFactory inputDataFolderReaderFactory, IInvertedIndex inputInvertedIndex, ISearchStrategyFactory inputSearchStrategyFactory);
    HashSet<ISearchable> Search(Query query);
}