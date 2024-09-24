using FullTextsearch.Document;
using FullTextsearch.Document.Formater;
using FullTextsearch.Document.Formater.Abstraction;
using FullTextsearch.Exceptions;
using FullTextsearch.Utility.Abstractions;
using Moq;
using NSubstitute;

namespace FullTextSearch.Test.Document;

public class DocumentFolderReaderTests
{
    private readonly DocumentFolderReader _sut;
    
    public DocumentFolderReaderTests()
    {
        _sut = new();
    }

    [Fact]
    public void ReadDataListFromFolder_ShouldReturnCorrectProperties_WhenFolderPathIsOK()
    {
        // Arrange
        var folderPath = "test";
        var dataMap = new Dictionary<string, (string, string)>
        {
            { "doc1", ("Name@#1", "Data&*1") },

            { "doc3", ("test", "") },

        };
        var textEditor = new Mock<ITextEditor>();
        var expectedResult = new List<FullTextsearch.Document.Document>
        {
            new ("Name@#1", "Data&*1", textEditor.Object),
            new FullTextsearch.Document.Document("test", "", textEditor.Object),
        };
        var fileDirectoryUtility = new Mock<IFileDirectoryUtility>();
        _sut.FileDirectoryUtility = fileDirectoryUtility.Object;
        fileDirectoryUtility.Setup(x => x.GetFiles(folderPath)).Returns(dataMap.Keys.ToArray);
        fileDirectoryUtility.Setup(x => x.GetFileName(It.IsAny<string>())).Returns<string>(input => dataMap[input].Item1);
        fileDirectoryUtility.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns<string>(input => dataMap[input].Item2);
        
        // Act
        var result = _sut.ReadDataListFromFolder(folderPath, textEditor.Object).ToList();
        
        // Assert
        for (int i = 0; i < 2; i++)
        {
            Assert.Equal(result[i], expectedResult[i]);
        }
    }
    
    [Fact]
    public void ReadDataListFromFolder_ShouldReturnDocumentList_WhenWhenFolderPathIsOK()
    {
        // Arrange
        var folderPath = "test";
        var dataMap = new Dictionary<string, (string, string)>
        {
            { "doc1", ("Name@#1", "Data&*1") },

            { "doc3", ("test", "") },

        };
        var textEditor = new Mock<ITextEditor>();
        var expectedResult = new List<object>
        {
           new object(),
           new object()
        };
        var fileDirectoryUtility = new Mock<IFileDirectoryUtility>();
        _sut.FileDirectoryUtility = fileDirectoryUtility.Object;
        fileDirectoryUtility.Setup(x => x.GetFiles(folderPath)).Returns(dataMap.Keys.ToArray);
        fileDirectoryUtility.Setup(x => x.GetFileName(It.IsAny<string>())).Returns<string>(input => dataMap[input].Item1);
        fileDirectoryUtility.Setup(x => x.ReadAllText(It.IsAny<string>())).Returns<string>(input => dataMap[input].Item2);
        
        // Act
        var result = _sut.ReadDataListFromFolder(folderPath, textEditor.Object).ToList();
        
        // Assert
        for (int i = 0; i < 2; i++)
        {
            Assert.NotEqual(result[i], expectedResult[i]);
        }
    }
    
    [Fact]
    public void ReadDataListFromFolder_ShouldReturnInvalidFolderPathException_WhenWhenFolderPathIsNotOK()
    {
        // Arrange
        var folderPath = "test";
        var textEditor = new Mock<ITextEditor>();
        var fileDirectoryUtility = new Mock<IFileDirectoryUtility>();
        _sut.FileDirectoryUtility = fileDirectoryUtility.Object;
        fileDirectoryUtility.Setup(x => x.GetFiles(folderPath)).Throws(new FileNotFoundException());
        
        // Act
        var action = () => _sut.ReadDataListFromFolder(folderPath, textEditor.Object).ToList();
        
        // Assert
        Assert.Throws<InvalidFolderPath>(action);
    }
    
    [Fact]
    public void ReadDataListFromFolder_ShouldReturnCorrectDataType_Whenenver()
    {
        // Arrange
        var expected = DataType.Document;
        
        // Act
        var result = _sut.DataType;
        
        // Assert
        Assert.Equal(expected, result);
    }
}