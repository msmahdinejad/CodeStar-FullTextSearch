using FullTextsearch.Document;
using FullTextsearch.Initialize;

namespace FullTextsearch.Service;

public interface IApiService
{
    HashSet<ISearchable> Search(string queryText);
    Task<Document.Document> AddDocument(IFormFile file);
}