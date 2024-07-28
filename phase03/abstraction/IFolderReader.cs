namespace phase02;
public interface IFolderReader
{
    IEnumerable<ISearchable> ReadFolder(string path);
    string GetClassName();
}