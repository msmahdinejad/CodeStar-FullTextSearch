using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;
using FullTextsearch.InvertedIndex;
using FullTextsearch.InvertedIndex.Abstraction;
using FullTextsearch.QueryModel;
using FullTextsearch.QueryModel.Abstraction;
using FullTextsearch.SearchManager.ResultList;
using FullTextsearch.SearchManager.ResultList.Abstraction;
using FullTextsearch.SearchManager.SignedSearchManager;
using FullTextsearch.WordFinder;
using FullTextsearch.WordFinder.Abstraction;
using Moq;

namespace FullTextSearch.Test.SearchManager;

public class SignedSearchControllerTests
{
    private readonly SignedSearchController _sut;
    private readonly Mock<ISignedSearchStrategy> _signedSearchStrategyMock;
    private readonly Mock<IInvertedIndex> _invertedIndexMock;
    private readonly Mock<IWordFinder> _unsignedWordFinderMock;
    private readonly Mock<IWordFinder> _positiveWordFinderMock;
    private readonly Mock<IWordFinder> _negativeWordFinderMock;
    private readonly Mock<IResultListMaker> _intersectresultListMakerMock;
    private readonly Mock<IResultListMaker> _unionresultListMakerMock;

    public SignedSearchControllerTests()
    {
        _signedSearchStrategyMock = new Mock<ISignedSearchStrategy>();
        _invertedIndexMock = new Mock<IInvertedIndex>();
        _unsignedWordFinderMock = new Mock<IWordFinder>();
        _unsignedWordFinderMock.Setup(w => w.Type).Returns(WordFinderType.Unsigned);
        _positiveWordFinderMock = new Mock<IWordFinder>();
        _positiveWordFinderMock.Setup(w => w.Type).Returns(WordFinderType.Positive);
        _negativeWordFinderMock = new Mock<IWordFinder>();
        _negativeWordFinderMock.Setup(w => w.Type).Returns(WordFinderType.Negative);
        _intersectresultListMakerMock = new Mock<IResultListMaker>();
        _intersectresultListMakerMock.Setup(w => w.Type).Returns(ResultListMakerType.Intersect);
        _unionresultListMakerMock = new Mock<IResultListMaker>();
        _unionresultListMakerMock.Setup(w => w.Type).Returns(ResultListMakerType.Union);

        _sut = new SignedSearchController(
            new[] { _unsignedWordFinderMock.Object, _positiveWordFinderMock.Object, _negativeWordFinderMock.Object },
            new[] { _intersectresultListMakerMock.Object, _unionresultListMakerMock.Object},
            _signedSearchStrategyMock.Object
        );
    }

    [Fact]
    public void SearchWithQuery_ShouldCallGetResult_WhenCalled()
    {
        // Arrange
        var inputQuery = new Mock<IQuery>();
        inputQuery.Setup(q => q.SplitedText).Returns(new[] { "test" });

        var unsignedResults = new HashSet<string> { "unsigned1", "unsigned2" };
        var positiveResults = new HashSet<string> { "positive1" };
        var negativeResults = new HashSet<string> { "negative1" };

        _unsignedWordFinderMock.Setup(w => w.FindWords(It.IsAny<IEnumerable<string>>()))
            .Returns(unsignedResults);
        _positiveWordFinderMock.Setup(w => w.FindWords(It.IsAny<IEnumerable<string>>()))
            .Returns(positiveResults);
        _negativeWordFinderMock.Setup(w => w.FindWords(It.IsAny<IEnumerable<string>>()))
            .Returns(negativeResults);

        var unSignedResult = new HashSet<ISearchable> { new Mock<ISearchable>().Object };
        var positiveResult = new HashSet<ISearchable> { new Mock<ISearchable>().Object };
        var negativeResult = new HashSet<ISearchable> { new Mock<ISearchable>().Object };

        _intersectresultListMakerMock.Setup(r => r.MakeResultList(unsignedResults, _invertedIndexMock.Object))
            .Returns(unSignedResult);
        _unionresultListMakerMock.Setup(r => r.MakeResultList(positiveResults, _invertedIndexMock.Object))
            .Returns(positiveResult);
        _unionresultListMakerMock.Setup(r => r.MakeResultList(negativeResults, _invertedIndexMock.Object))
            .Returns(negativeResult);

        _signedSearchStrategyMock.Setup(s => s.GetResult(
            unsignedResults,
            positiveResults,
            negativeResults,
            unSignedResult,
            positiveResult,
            negativeResult,
            It.IsAny<HashSet<ISearchable>>()
        ));

        // Act
        _sut.SearchWithQuery(inputQuery.Object, _invertedIndexMock.Object);

        // Assert
        _signedSearchStrategyMock.Verify(s => s.GetResult(
            unsignedResults,
            positiveResults,
            negativeResults,
            unSignedResult,
            positiveResult,
            negativeResult,
            It.IsAny<HashSet<ISearchable>>()
        ), Times.Once);
    }
}