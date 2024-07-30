using NSubstitute;
using phase02.Document;
using phase02.Document.Formater;
using phase02.Utility.Abstractions;

namespace TestProject.Document;

public class DocumentFolderReaderTests
{
    private readonly DocumentFolderReader _sut;
    private readonly IFileDirectoryUtility _fileDirectoryUtility;
    private readonly ITextEditor _textEditor;
    
    public DocumentFolderReaderTests()
    {
        _textEditor = Substitute.For<ITextEditor>();
        _fileDirectoryUtility = Substitute.For<IFileDirectoryUtility>();
        _sut = new();
    }

    [Fact]
    public void ReadDataListFromFolder_ShouldReturnCorrectList_WhenFolderPathIsOK()
    {
        // Arrange
        var folderPath = "test";
        var dataMap = new Dictionary<string, (string, string)>
        {
            { "doc1", ("Name@#1", "Data&*1") },

            { "doc2", ("SimpleName", "SimpleData123") },

            { "doc3", ("test", "") },

            { "test", ("NameWithSpaces", "DataWith@Special&Chars") },

        };
        var expectedResult = new List<phase02.Document.Document>
        {
            new phase02.Document.Document("Name@#1", "Data&*1", _textEditor),
            new phase02.Document.Document("SimpleName", "SimpleData123", _textEditor),
            new phase02.Document.Document("test", "", _textEditor),
            new phase02.Document.Document("NameWithSpaces", "DataWith@Special&Chars", _textEditor),
        };
        _sut.FileDirectoryUtility = _fileDirectoryUtility;
        _fileDirectoryUtility.GetFiles(folderPath).Returns(new string []{"doc1", "doc2", "doc3", "test"});
        foreach (var key in dataMap.Keys)
        {
            _fileDirectoryUtility.GetFileName(key).Returns(dataMap[key].Item1);
            _fileDirectoryUtility.ReadAllText(key).Returns(dataMap[key].Item2);
        }
        
        // Act
        var result = _sut.ReadDataListFromFolder(folderPath, _textEditor);

        // Assert
        Assert.Equal(result.First(), expectedResult.First());
    }
}