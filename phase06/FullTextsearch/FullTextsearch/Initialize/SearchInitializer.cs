using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;
using FullTextsearch.Document.Extractor;
using FullTextsearch.Document.Extractor.Abstraction;
using FullTextsearch.Document.Formater;
using FullTextsearch.Factory.FolderFactory;
using FullTextsearch.Factory.SearchFactory;
using FullTextsearch.Factory.SearchFactory.Abstraction;
using FullTextsearch.Initialize.Abstarction;
using FullTextsearch.InvertedIndex;
using FullTextsearch.InvertedIndex.Abstraction;
using FullTextsearch.QueryModel;
using FullTextsearch.QueryModel.Abstraction;
using FullTextsearch.SearchManager;
using FullTextsearch.SearchManager.Abstraction;
using Microsoft.AspNetCore.Mvc;

namespace FullTextsearch.Initialize;

public class SearchInitializer(
    IInvertedIndex invertedIndex,
    ISearchStrategyFactory inputSearchStrategyFactory,
    IExtractor extractor)
    : ISearchInitializer
{
    public ISearchController SearchController { get; set; }

    public void Build(SearchStrategyType searchType)
    {
        SearchController = inputSearchStrategyFactory.MakeSearchController(searchType);
    }

    public HashSet<ISearchable> Search(IQuery query)
    {
        return SearchController.SearchWithQuery(query, invertedIndex);
    }

    public void AddData(ISearchable data)
    {
        invertedIndex.AddDataToMap(data, extractor);
    }
}