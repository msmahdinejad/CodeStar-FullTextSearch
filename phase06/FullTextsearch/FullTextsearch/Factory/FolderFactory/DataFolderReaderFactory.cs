using FullTextsearch.Document;
using FullTextsearch.Exceptions;

namespace FullTextsearch.Factory.FolderFactory;

public class DataFolderReaderFactory : IDataFolderReaderFactory
{
    private IEnumerable<IDataFolderReader> _folderReaderList { get; set; }

    public DataFolderReaderFactory(IEnumerable<IDataFolderReader> folderReaderList) =>
        _folderReaderList = folderReaderList;

    public IDataFolderReader MakeDataFolderReader(DataType className)
    {
        var folderReader = _folderReaderList.SingleOrDefault(x => x.DataType == className);
        return folderReader ?? throw new InvalidClassName();
    }
}