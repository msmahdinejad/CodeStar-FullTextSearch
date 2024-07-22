namespace phase02;

public class Program
{
    public static void Main()
    {
        var myDoucumentReader = new DocumentReader();
        myDoucumentReader.RaedFolder();
        var mySearchController = new SearchController();
        var word = Console.ReadLine();
        foreach (var docName in mySearchController.search(word))
        {
            Console.WriteLine(docName);
        }
    }
}