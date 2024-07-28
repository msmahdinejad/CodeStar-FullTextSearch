namespace phase02;
public interface IFolderReaderFactory
{
    IEnumerable<ISearchable> ReadFolderDataList(string className, string folderPath);
}