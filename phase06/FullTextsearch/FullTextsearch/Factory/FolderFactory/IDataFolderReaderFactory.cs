using FullTextsearch.Document;

namespace FullTextsearch.Factory.FolderFactory;

public interface IDataFolderReaderFactory
{
    IDataFolderReader MakeDataFolderReader(DataType className);
}