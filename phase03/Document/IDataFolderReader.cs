namespace phase02;

public interface IDataFolderReader
{
    IEnumerable<ISearchable> ReadDataListFromFolder(string path);
    string GetClassName();
}