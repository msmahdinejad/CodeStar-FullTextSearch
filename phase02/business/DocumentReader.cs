using System.Reflection;

namespace phase02;
public class DocumentReader : IDataReader
{
    public void RaedFolder()
    {
        var folderPath = resources.Resource1.folderPath;

        var files = Directory.GetFiles(folderPath);
        foreach (var file in files)
        {
            var data = RaedData(file);
            var name = Path.GetFileName(file);
            InvertedIndexController myInvertedIndex = InvertedIndexController.Instance;
            myInvertedIndex.AddTextToMap(name, data);
        }
    }

    public string RaedData(string path)
    {
        return File.ReadAllText(path);
    }
}