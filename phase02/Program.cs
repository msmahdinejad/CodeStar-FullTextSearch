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
            while (true)
            {
                var queryText = Console.ReadLine();
                var query = new QueryController(queryText);
                var result = query.RunQuery();
                if(result.Count == 0)
                {
                    Console.WriteLine("Not found!");
                }
                foreach (var docName in result)
                {
                    Console.WriteLine(docName);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}