using phase02.Document;
using phase02.QueryModel;
using phase02.SearchManager;

namespace phase02.Initialize;

public interface ISearchInitializer
{
    void Build(DataType className, string folderPath, SearchStrategyType searchType);
    HashSet<ISearchable> Search(IQuery query);
    
}