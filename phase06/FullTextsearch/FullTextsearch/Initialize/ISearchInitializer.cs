using FullTextsearch.Document;
using FullTextsearch.QueryModel;
using FullTextsearch.SearchManager;

namespace FullTextsearch.Initialize;

public interface ISearchInitializer
{
    void Build(DataType className, string folderPath, SearchStrategyType searchType);
    HashSet<ISearchable> Search(IQuery query);
    
}