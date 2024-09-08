using phase02.Document.Formater;

namespace phase02.Document;

public interface IDataFolderReader
{
    DataType DataType { get; }
    IEnumerable<ISearchable> ReadDataListFromFolder(string path, ITextEditor textEditor);
}