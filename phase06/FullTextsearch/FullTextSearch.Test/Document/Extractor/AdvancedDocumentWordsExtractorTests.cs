using Moq;
using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;
using FullTextsearch.Document.Extractor;

namespace FullTextSearch.Test.Document.Extractor;

public class AdvancedDocumentWordsExtractorTests
{
    [Fact]
    public void GetKey_ShouldCallGetWordsMethod_Whenenver()
    {
        // Arrange
        var sut = new AdvancedDocumentWordsExtractor();
        var dataMock = new Mock<ISearchable>();
        dataMock.Setup(x => x.GetWords()).Returns(new List<string>());
        
        // Act
        sut.GetKey(dataMock.Object);
        
        // Assert
        dataMock.Verify(x => x.GetWords(), Times.Once);
    }
    
    [Fact]
    public void GetKey_ShouldReturnCorrectList_Whenenver()
    {
        // Arrange
        var sut = new AdvancedDocumentWordsExtractor();
        var dataMock = new Mock<ISearchable>();
        dataMock.Setup(x => x.GetWords()).Returns(new List<string>(){"1" , "2" , "3"});
        var expectedResult = new List<string>() { "1", "2", "3", "1 2", "2 3", "1 2 3" };
        
        // Act
        var result = sut.GetKey(dataMock.Object);
        
        // Assert
        Assert.Equal(result, expectedResult);
    }
}