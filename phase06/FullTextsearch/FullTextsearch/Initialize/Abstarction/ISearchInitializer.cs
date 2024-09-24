using FullTextsearch.Document.Abstraction;
using FullTextsearch.QueryModel.Abstraction;
using FullTextsearch.SearchManager;

namespace FullTextsearch.Initialize.Abstarction;

public interface ISearchInitializer
{
    void Build(SearchStrategyType searchType);
    HashSet<ISearchable> Search(IQuery query);

    void AddData(ISearchable data);
}