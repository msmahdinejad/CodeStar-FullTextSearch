namespace phase02;

public interface IDataFolderReader
{
    DataType DataType { get; }
    IEnumerable<ISearchable> ReadDataListFromFolder(string path);
}