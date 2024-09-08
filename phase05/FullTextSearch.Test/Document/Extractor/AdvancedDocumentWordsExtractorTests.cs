using Moq;
using phase02.Document;
using phase02.Document.Extractor;

namespace FullTextSearch.Test.Document.Extractor;

public class AdvancedDocumentWordsExtractorTests
{
    private AdvancedDocumentWordsExtractor sut;
    private Mock<ISearchable> dataMock;

    public AdvancedDocumentWordsExtractorTests()
    {
        sut = new AdvancedDocumentWordsExtractor();
        dataMock = new Mock<ISearchable>();
    }
    
    [Fact]
    public void GetKey_ShouldCallGetWordsMethod_Whenenver()
    {
        // Arrange
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
        dataMock.Setup(x => x.GetWords()).Returns(new List<string>(){"1" , "2" , "3"});
        var expectedResult = new List<string>() { "1", "2", "3", "1 2", "2 3", "1 2 3" };
        
        // Act
        var result = sut.GetKey(dataMock.Object);
        
        // Assert
        Assert.Equal(result, expectedResult);
    }
}