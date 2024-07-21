using System.Dynamic;
using System.Text.Json;

namespace phase01;

public class JsonReader<T> : IDataReader<T>
{
    public async Task<List<T>> Read()
    {
        var path = GetJson();
        var jsonData = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<List<T>>(jsonData);
    }

    public string GetJson()
    {
        var jasonPath = Console.ReadLine();
        return jasonPath;
    }
}