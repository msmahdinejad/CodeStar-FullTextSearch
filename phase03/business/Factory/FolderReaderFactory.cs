using System.Net.Mail;

namespace phase02;
public class FolderReaderFactory : IFolderReaderFactory
{
    private List<IFolderReader> _folderReaderList { get; set; }
    public FolderReaderFactory(List<IFolderReader> folderReaderList) => _folderReaderList = folderReaderList;
    public IEnumerable<ISearchable> ReadFolderDataList(string className, string folderPath)
    {
        var dataList = _folderReaderList.SingleOrDefault(x => x.GetClassName() == className).ReadFolder(folderPath);
        return dataList ??throw new Exception("Search Strategy not found!");
    }
}