using FullTextsearch.Document;
using FullTextsearch.QueryModel;
using FullTextsearch.SearchManager;

namespace FullTextsearch.Initialize;

public interface ISearchInitializer
{
    void Build(SearchStrategyType searchType);
    HashSet<ISearchable> Search(IQuery query);

    void AddData(ISearchable data);
}