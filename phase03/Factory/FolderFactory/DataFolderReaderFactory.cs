using System.Net.Mail;
using phase02.Exceptions;

namespace phase02;

public class DataFolderReaderFactory : IDataFolderReaderFactory
{
    private List<IDataFolderReader> _folderReaderList { get; set; }

    public DataFolderReaderFactory(List<IDataFolderReader> folderReaderList) =>
        _folderReaderList = folderReaderList;

    public IDataFolderReader ReadDataListFromFolder(string className)
    {
        var folderReader = _folderReaderList.SingleOrDefault(x => x.GetClassName() == className);
        return folderReader ?? throw new InvalidClassName();
    }
}