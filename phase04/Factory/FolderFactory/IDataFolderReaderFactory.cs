namespace phase02;

public interface IDataFolderReaderFactory
{
    IDataFolderReader ReadDataListFromFolder(DataType className);
}