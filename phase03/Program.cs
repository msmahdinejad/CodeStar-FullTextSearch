namespace phase02;

public class Program
{
    public static void Main()
    {
        try
        {
            var folderPath = Console.ReadLine();
            var className = Console.ReadLine();
            var processor = new Processor(folderPath, className, "SignedSearch");
            processor.Build();
            var queryText = Console.ReadLine();
            while (queryText != "exit")
            {
                var query = new Query(queryText);
                var result = processor.Search(query);
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