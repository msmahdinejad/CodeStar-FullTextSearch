namespace phase02;

public class Program
{
    public static void Main()
    {
        try
        {
            var myDoucumentReader = new DocumentReader();
            var folderPath = resources.Resource1.folderPath;
            var dataList = myDoucumentReader.RaedFolder(folderPath);
            var invertedIndex = new InvertedIndexController();
            invertedIndex.AddListToMap(dataList);
            var searchcontroller = new SearchWithSign(invertedIndex);
            var queryText = Console.ReadLine();
            while (queryText != "exit")
            {
                var query = new Query(queryText);
                var result = searchcontroller.SearchWithQuery(query);
                if (result.Count == 0)
                {
                    Console.WriteLine("Not found!");
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