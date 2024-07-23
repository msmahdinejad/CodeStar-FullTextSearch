using System.ComponentModel;
using System.Reflection;

namespace phase02;
public class DocumentReader : IDataReader
{
    private void AddData(string file)
    {
        var data = RaedData(file);
        var name = Path.GetFileName(file);
        MyInvertedIndex.AddTextToMap(name, data);
    }
    public InvertedIndexController MyInvertedIndex {get; init;}
    public DocumentReader()
    {
        this.MyInvertedIndex = InvertedIndexController.Instance;
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
        catch(FileNotFoundException)
        {
            throw new Exception("Path not found!");
        }

    }

    public string RaedData(string path)
    {
        return File.ReadAllText(path);
    }
}