using System.ComponentModel;
using System.Reflection;
using phase02.Exceptions;
using phase02.resources;

namespace phase02;

public class DocumentFolderReader : IDataFolderReader
{
    private static DocumentFolderReader _documentFolderReader;
    private const string _FolderName = "Document";
    public static DocumentFolderReader Instance => _documentFolderReader ??= new DocumentFolderReader();

    public IEnumerable<ISearchable> ReadDataListFromFolder(string path)
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
            throw new InvalidFolderPath();
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