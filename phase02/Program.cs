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
            var mySearchController = new SearchController();
            var word = Console.ReadLine();
            foreach (var docName in mySearchController.Search(word))
            {
                Console.WriteLine(docName);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
    }
}