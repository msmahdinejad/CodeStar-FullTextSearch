using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;

namespace FullTextsearch.Factory.FolderFactory.Abstraction;

public interface IDataFolderReaderFactory
{
    IDataFolderReader MakeDataFolderReader(DataType className);
}