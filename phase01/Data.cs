using System.Text.Json;

namespace phase01;

public class Json<T>{
    public async Task<List<T>> ReadJsonFile(string path)
    {
        var jsonData = await File.ReadAllTextAsync(path);
        return JsonSerializer.Deserialize<List<T>>(jsonData);
    }
}