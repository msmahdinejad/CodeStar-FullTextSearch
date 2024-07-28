namespace phase02;

public interface IDataFolderReaderFactory
{
    IDataFolderReader ReadDataListFromFolder(string className);
}