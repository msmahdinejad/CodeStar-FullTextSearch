namespace phase02;

public class Program
{
    public static void Main()
    {
        try
        {
            var myDoucumentReader = new DocumentReader();
            var folderPath = resources.Resource1.folderPath;
            myDoucumentReader.RaedFolder(folderPath);
            var queryText = Console.ReadLine();
            while (queryText != "exit")
            {
                var query = new QueryController(queryText);
                var result = query.RunQuery();
                if (result.Count == 0)
                {
                    Console.WriteLine("Not found!");
                }
                foreach (var docName in result)
                {
                    Console.WriteLine(docName);
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