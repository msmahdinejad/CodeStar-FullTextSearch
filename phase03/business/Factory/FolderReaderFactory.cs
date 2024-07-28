using System.Net.Mail;

namespace phase02;
public class FolderReaderFactory : IFolderReaderFactory
{
    private static FolderReaderFactory _folderReaderFactory;
    public static FolderReaderFactory Instance => _folderReaderFactory ??= new FolderReaderFactory();
    private Dictionary<string, IFolderReader> _map { get; set; }

    private FolderReaderFactory()
    {
        _map = new Dictionary<string, IFolderReader>()
        {
            {"Document", DocumentReader.Instance}
        };
    }
    public IEnumerable<ISearchable> ReadFolderDataList(string className, string folderPath)
    {
        if (_map.ContainsKey(className))
            return _map[className].RaedFolder(folderPath);
        else
            throw new Exception("Class not found!");
    }
}