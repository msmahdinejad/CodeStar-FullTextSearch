using System.ComponentModel;
using System.Reflection;

namespace phase02;
public class DocumentReader : IDataReader
{
    private InvertedIndexController MyInvertedIndex { get; init; }
    public DocumentReader()
    {
        MyInvertedIndex = InvertedIndexController.Instance;
    }
    private void AddData(string file)
    {
        var data = RaedData(file);
        var name = Path.GetFileName(file);
        MyInvertedIndex.AddTextToMap(name, data);
    }
    public void RaedFolder(string path)
    {

        try
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                AddData(file);
            }
        }
        catch (FileNotFoundException)
        {
            throw new Exception("Path not found!");
        }

    }

    private string RaedData(string path)
    {
        return File.ReadAllText(path);
    }
}