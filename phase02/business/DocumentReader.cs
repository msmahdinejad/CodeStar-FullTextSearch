using System.ComponentModel;
using System.Reflection;

namespace phase02;
public class DocumentReader : IFolderReader
{
    public IEnumerable<ISearchable> RaedFolder(string path)
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
}