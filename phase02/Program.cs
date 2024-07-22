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
                foreach (var docName in query.RunQuery())
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