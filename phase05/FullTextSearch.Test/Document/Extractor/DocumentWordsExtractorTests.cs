using Moq;
using NSubstitute.ReceivedExtensions;
using phase02.Document;
using phase02.Document.Extractor;

namespace FullTextSearch.Test.Document.Extractor;

public class DocumentWordsExtractorTests
{
    [Fact]
    public void GetKey_ShouldCallGetWordsMethod_Whenenver()
    {
        // Arrange
        var sut = new DocumentWordsExtractor();
        var dataMock = new Mock<ISearchable>();
        dataMock.Setup(x => x.GetWords()).Returns(new List<string>());
        
        // Act
        sut.GetKey(dataMock.Object);
        
        // Assert
        dataMock.Verify(x => x.GetWords(), Times.Once);
    }
}