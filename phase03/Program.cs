using phase02.resources;

namespace phase02;

public class Program
{
    private const string _searchStrategy = "SignedSearch";
    private const string _className = "Document";
    private const string _endQuery = "exit";
    private const string _invalidKeyMessage = "Key Not Found!";


    public static void Main()
    {
        try
        {
            var folderPath = Console.ReadLine();
            var processor = new Initializer();
            processor.Build(_className, folderPath, _searchStrategy,
                new DataFolderReaderFactory(new List<IDataFolderReader>() { new DocumentFolderReader() }),
                new InvertedIndexController(),
                new SearchStrategyFactory(new List<ISearchStrategy>() { new SignedSearchStrategy() }));

            var queryText = Console.ReadLine();
            while (queryText != _endQuery)
            {
                var query = new Query(queryText);
                var result = processor.Search(query);
                if (result.Count == 0)
                {
                    Console.WriteLine(_invalidKeyMessage);
                }

                foreach (var data in result)
                {
                    Console.WriteLine(data.GetValue());
                }

                queryText = Console.ReadLine();
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}