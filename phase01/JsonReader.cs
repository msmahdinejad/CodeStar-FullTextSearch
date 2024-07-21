using System.Reflection;
using System.Resources;
using System.Text.Json;

namespace phase01;

public class JsonReader<T> : IDataReader<T>
{
    public async Task<List<T>> Read()
    {
        var listName = GetJson();
        //var jsonData = await File.ReadAllTextAsync(path);
        ResourceManager resourceManager = new ResourceManager($"phase01.{listName}", Assembly.GetExecutingAssembly());
        string jsonData = resourceManager.GetString(listName);
        return JsonSerializer.Deserialize<List<T>>(jsonData);
    }

    public string GetJson()
    {
        var jasonPath = Console.ReadLine();
        return jasonPath;
    }
}