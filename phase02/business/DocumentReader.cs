using System.Reflection;

namespace phase02;
public class DocumentReader : IDataReader
{
    InvertedIndexController myInvertedIndex;
    public DocumentReader()
    {
        this.myInvertedIndex = InvertedIndexController.Instance;
    }
    public void RaedFolder(string path)
    {

        var files = Directory.GetFiles(path);
        foreach (var file in files)
        {
            var data = RaedData(file);
            var name = Path.GetFileName(file);

            myInvertedIndex.AddTextToMap(name, data);
        }

    }

    public string RaedData(string path)
    {
        return File.ReadAllText(path);
    }
}