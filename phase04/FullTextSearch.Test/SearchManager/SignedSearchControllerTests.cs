using Moq;
using phase02.Document;
using phase02.Document.Formater;
using phase02.InvertedIndex;
using phase02.QueryManager.WordFinder;
using phase02.QueryModel;
using phase02.SearchManager;
using phase02.SearchManager.ResultList;

namespace FullTextSearch.Test.SearchManager;

public class SignedSearchControllerTests
{
    [Fact]
    public void SearchWithQuery_ShouldCallCorrectMethods_Whenever()
    {
        //Arrange

        var unSignedInputWordsMock = new Mock<IWordFinder>();
        var positiveInputWordsMock = new Mock<IWordFinder>();
        var negativeInputWordsMock = new Mock<IWordFinder>();
        var intersectResultListMakerMock = new Mock<IResultListMaker>();
        var unionResultListMakerMock = new Mock<IResultListMaker>();
        var signedSearchStrategyMock = new Mock<ISignedSearchStrategy>();
        var invertedIndexMock = new Mock<IInvertedIndex>();
        var queryMock = new Mock<IQuery>();

        var sut = new SignedSearchController(unSignedInputWordsMock.Object, positiveInputWordsMock.Object,
            negativeInputWordsMock.Object, intersectResultListMakerMock.Object, unionResultListMakerMock.Object,
            signedSearchStrategyMock.Object);
        
        //Act
        sut.SearchWithQuery(queryMock.Object, invertedIndexMock.Object);

        //Assert
        unSignedInputWordsMock.Verify(x => x.FindWords(queryMock.Object.SplitedText), Times.Once);
        positiveInputWordsMock.Verify(x => x.FindWords(queryMock.Object.SplitedText), Times.Once);
        negativeInputWordsMock.Verify(x => x.FindWords(queryMock.Object.SplitedText), Times.Once);
        intersectResultListMakerMock.Verify(x => x.MakeResultList(It.IsAny<HashSet<string>>(), invertedIndexMock.Object), Times.Once);
        unionResultListMakerMock.Verify(x => x.MakeResultList(It.IsAny<HashSet<string>>(), invertedIndexMock.Object), Times.Exactly(2));
        signedSearchStrategyMock.Verify(x =>
            x.GetResult(It.IsAny<HashSet<ISearchable>>(), It.IsAny<HashSet<ISearchable>>(), It.IsAny<HashSet<ISearchable>>(), invertedIndexMock.Object.GetAllValue()), Times.Once);
    }
}