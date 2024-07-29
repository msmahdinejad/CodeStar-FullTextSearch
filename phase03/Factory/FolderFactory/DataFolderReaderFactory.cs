using System.Net.Mail;
using phase02.Exceptions;

namespace phase02;

public class DataFolderReaderFactory : IDataFolderReaderFactory
{
    private List<IDataFolderReader> _folderReaderList { get; set; }

    public DataFolderReaderFactory(List<IDataFolderReader> folderReaderList) =>
        _folderReaderList = folderReaderList;

    public IDataFolderReader ReadDataListFromFolder(DataType className)
    {
        var folderReader = _folderReaderList.SingleOrDefault(x => x.DataType == className);
        return folderReader ?? throw new InvalidClassName();
    }
}