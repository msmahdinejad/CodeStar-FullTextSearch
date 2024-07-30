namespace phase02.Document;

public interface IDataFolderReader
{
    DataType DataType { get; }
    IEnumerable<ISearchable> ReadDataListFromFolder(string path);
}