namespace phase02;

public interface IProcessor
{
    void Build(string className, string folderPath, string searchType, FolderReaderFactory inputFolderReaderFactory, IInvertedIndex inputInvertedIndex, SearchStrategyFactory inputSearchStrategyFactory);
    HashSet<ISearchable> Search(Query query);
}