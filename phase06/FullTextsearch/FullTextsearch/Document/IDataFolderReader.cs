using FullTextsearch.Document.Formater;

namespace FullTextsearch.Document;

public interface IDataFolderReader
{
    DataType DataType { get; }
    IEnumerable<ISearchable> ReadDataListFromFolder(string path, ITextEditor textEditor);
}