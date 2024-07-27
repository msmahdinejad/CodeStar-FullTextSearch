namespace phase02;
public interface IFolderReader
{
    IEnumerable<ISearchable> RaedFolder(string path);
}