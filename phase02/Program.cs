namespace phase02;

public class Program
{
    public static void Main()
    {
        try
        {
            var folderPath = Console.ReadLine();
            var className = Console.ReadLine();
            IEnumerable<ISearchable> dataList = null;
            while (true)
            {
                if (FolderReaderFactory.Instance.Map.ContainsKey(className))
                {
                    dataList = FolderReaderFactory.Instance.Map[className].RaedFolder(folderPath);
                    break;
                }
                else
                {
                    Console.WriteLine("Class not found!");
                    className = Console.ReadLine();
                }
            }
            var invertedIndex = new InvertedIndexController();
            invertedIndex.AddDataListToMap(dataList);
            var searchcontroller = new SignedSearchStrategy(invertedIndex);
            var queryText = Console.ReadLine();
            while (queryText != "exit")
            {
                var query = new Query(queryText);
                var result = searchcontroller.SearchWithQuery(query);
                if (result.Count == 0)
                {
                    Console.WriteLine("Key not found!");
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