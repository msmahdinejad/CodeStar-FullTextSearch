using phase02.Document;

namespace phase02.Factory.FolderFactory;

public interface IDataFolderReaderFactory
{
    IDataFolderReader MakeDataFolderReader(DataType className);
}