using System.ComponentModel;
using System.Reflection;

namespace phase02;
public class DocumentReader : IFolderReader
{
    private static DocumentReader _DocumentReader;
    private const string _FolderName = "Document";
    public static DocumentReader Instance => _DocumentReader ??= new DocumentReader();
    public IEnumerable<ISearchable> ReadFolder(string path)
    {
        var documentsList = new List<Document>();
        try
        {
            var files = Directory.GetFiles(path);
            foreach (var file in files)
            {
                var data = RaedData(file);
                var name = Path.GetFileName(file);
                documentsList.Add(new Document(name, data));
            }
        }
        catch (FileNotFoundException)
        {
            throw new Exception("Path not found!");
        }
        return documentsList;
    }
    private string RaedData(string path)
    {
        return File.ReadAllText(path);
    }
    public string GetClassName()
    {
        return _FolderName;
    }
}