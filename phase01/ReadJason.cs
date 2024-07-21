using System.Dynamic;
using System.Text.Json;

namespace phase01;

public class ReadJason<T> : IReadData<T>
{
    public async Task<List<T>> Read()
    {
        var path = GetJason();
        var jsonData = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<List<T>>(jsonData);
    }

    public string GetJason()
    {
        var jasonPath = Console.ReadLine();
        return jasonPath;
    }
}