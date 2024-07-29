using phase02.resources;

namespace phase02;

public class Program
{
    private const string EndQuery = "Exit";
    private const string InvalidKeyMessage = "Key not found!";

    public static void Main()
    {
        try
        {
            var folderPath = Console.ReadLine();
            var processor = new SearchInitializer(new DataFolderReaderFactory([new DocumentFolderReader()]),
                new InvertedIndexController(),
                new SearchStrategyFactory([new SignedSearchStrategy()], new SignedSearchStrategy()),
                new SignedSearchStrategy());
            processor.Build(DataType.Document, folderPath, SearchStrategyType.SignedSearch
            );

            var queryText = Console.ReadLine();
            while (queryText != EndQuery)
            {
                var query = new Query(queryText);
                var result = processor.Search(query);
                if (result.Count == 0)
                {
                    Console.WriteLine(InvalidKeyMessage);
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