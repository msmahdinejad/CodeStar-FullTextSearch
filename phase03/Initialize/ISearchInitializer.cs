namespace phase02;

public interface ISearchInitializer
{
    void Build(DataType className, string folderPath, SearchStrategyType searchType);
    HashSet<ISearchable> Search(Query query);
    
}