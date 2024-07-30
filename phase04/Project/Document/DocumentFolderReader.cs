using phase02.Exceptions;

namespace phase02.Document;

public class DocumentFolderReader : IDataFolderReader
{
    
    private static DocumentFolderReader _documentFolderReader;
    public static DocumentFolderReader Instance => _documentFolderReader ??= new DocumentFolderReader();

    public DataType DataType => DataType.Document;

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
}