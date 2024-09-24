using FullTextsearch.Document.Abstraction;

namespace FullTextsearch.Service.Abstraction;

public interface IApiService
{
    HashSet<ISearchable> Search(string queryText);
    Task<Document.Document> AddDocument(IFormFile file);
}