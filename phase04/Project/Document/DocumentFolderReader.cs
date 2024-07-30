using phase02.Document.Formater;
using phase02.Exceptions;
using phase02.Utility;
using phase02.Utility.Abstractions;

namespace phase02.Document;

public class DocumentFolderReader : IDataFolderReader
{
    public IFileDirectoryUtility FileDirectoryUtility { get; set; }

    public DocumentFolderReader()
    {
        FileDirectoryUtility = new FileDirectoryUtility();
    }

    public DataType DataType => DataType.Document;

    public IEnumerable<ISearchable> ReadDataListFromFolder(string path, ITextEditor textEditor)
    {
        var documentsList = new List<Document>();
        try
        {
            var filesPath = FileDirectoryUtility.GetFiles(path);
            foreach (var filePath in filesPath)
            {
                var data = FileDirectoryUtility.ReadAllText(filePath);
                var name = FileDirectoryUtility.GetFileName(filePath);
                documentsList.Add(new Document(name, data, textEditor));
            }
        }
        catch (FileNotFoundException)
        {
            throw new InvalidFolderPath();
        }

        return documentsList;
    }
    
}