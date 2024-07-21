using System.Text.Json;

public class Json<T>{
    public static async Task<List<T>> ReadStudentsFromJsonFile(string path)
    {
        string JsonData = await File.ReadAllTextAsync(path);
        List<T> list = JsonSerializer.Deserialize<List<T>>(JsonData);
        return list;
    }
}