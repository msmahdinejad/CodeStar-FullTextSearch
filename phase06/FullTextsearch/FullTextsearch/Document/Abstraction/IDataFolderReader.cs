using FullTextsearch.Document.Formater.Abstraction;

namespace FullTextsearch.Document.Abstraction;

public interface IDataFolderReader
{
    DataType DataType { get; }
    IEnumerable<ISearchable> ReadDataListFromFolder(string path, ITextEditor textEditor);
}