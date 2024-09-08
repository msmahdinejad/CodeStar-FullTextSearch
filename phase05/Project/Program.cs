using phase02.Document;
using phase02.Document.Extractor;
using phase02.Document.Formater;
using phase02.Factory.FolderFactory;
using phase02.Factory.SearchFactory;
using phase02.Initialize;
using phase02.InvertedIndex;
using phase02.QueryManager.WordFinder;
using phase02.QueryModel;
using phase02.SearchManager;
using phase02.SearchManager.ResultList;
using phase02.SearchManager.SignedSearchManager;

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
                new SearchStrategyFactory([new SignedSearchController(new NegativeWordFinder(), new PositiveWordFinder(), new UnsignedWordFinder(), new IntersectResultListMaker(), new UnionResultListMaker(), new SignedSearchStrategy())]),
                new DocumentTextEditor(), new AdvancedDocumentWordsExtractor());
            processor.Build(DataType.Document, folderPath, SearchStrategyType.SignedSearch
            );

            var queryText = Console.ReadLine();
            while (queryText != EndQuery)
            {
                var query = new AdvancedQuery(queryText);
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