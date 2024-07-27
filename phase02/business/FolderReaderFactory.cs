using System.Net.Mail;

namespace phase02;
public class FolderReaderFactory
{
    private static FolderReaderFactory _folderReaderFactory;
    public static FolderReaderFactory Instance => _folderReaderFactory ??= new FolderReaderFactory();
    public Dictionary<string, IFolderReader> Map {get; set;}

    private FolderReaderFactory()
    {
        Map = new Dictionary<string, IFolderReader>()
        {
            {"Document", DocumentReader.Instance}
        };
    }
}