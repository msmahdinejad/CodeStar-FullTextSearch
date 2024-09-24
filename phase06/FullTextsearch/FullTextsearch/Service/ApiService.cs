using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;
using FullTextsearch.Document.Formater;
using FullTextsearch.Initialize;
using FullTextsearch.Initialize.Abstarction;
using FullTextsearch.QueryModel;
using FullTextsearch.SearchManager;
using FullTextsearch.Service.Abstraction;

namespace FullTextsearch.Service;

public class ApiService : IApiService
{
    private ISearchInitializer _searchInitializer;

    public ApiService(ISearchInitializer searchInitializer)
    {
        _searchInitializer = searchInitializer;
        _searchInitializer.Build(SearchStrategyType.SignedSearch);
    }

    public HashSet<ISearchable> Search(string queryText)
    {
        var query = new AdvancedQuery(queryText);
        return _searchInitializer.Search(query);
    }

    public async Task<Document.Document> AddDocument(IFormFile file)
    {
        var fileName = file.FileName;
        
        string fileContent;
        using (var reader = new StreamReader(file.OpenReadStream()))
        {
            fileContent = await reader.ReadToEndAsync();
        }

        var data = new Document.Document(fileName, fileContent, new DocumentTextEditor());
        
        _searchInitializer.AddData(data);

        return data;
    }
}