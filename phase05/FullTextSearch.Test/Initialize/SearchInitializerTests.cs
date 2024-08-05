using Moq;
using NSubstitute;
using phase02.Document;
using phase02.Document.Formater;
using phase02.Factory.FolderFactory;
using phase02.Factory.SearchFactory;
using phase02.Initialize;
using phase02.InvertedIndex;
using phase02.QueryManager.WordFinder;
using phase02.QueryModel;
using phase02.SearchManager;
using phase02.SearchManager.ResultList;

namespace FullTextSearch.Test.Initialize;

public class SearchInitializerTests
{
    [Fact]
    public void Build_ShouldCallCorrectMethods_Whenever()
    {
        //Arrange
        var inputDataFolderReaderFactoryMock = new Mock<IDataFolderReaderFactory>();
        var dataFolderReaderMock = new Mock<IDataFolderReader>();
        
        var invertedIndexMock = new Mock<IInvertedIndex>();
        var inputSearchStrategyFactoryMock = new Mock<ISearchStrategyFactory>();
        var textEditorMock = new Mock<ITextEditor>();
        var extractorMock = new Mock<IExtractor>();
        
        dataFolderReaderMock.Setup(x => x.ReadDataListFromFolder(It.IsAny<string>(), textEditorMock.Object))
            .Returns(new List<ISearchable>());
        inputDataFolderReaderFactoryMock.Setup(x => x.MakeDataFolderReader(DataType.Document))
            .Returns(dataFolderReaderMock.Object);

        var sut = new SearchInitializer(inputDataFolderReaderFactoryMock.Object, invertedIndexMock.Object, inputSearchStrategyFactoryMock.Object, textEditorMock.Object, extractorMock.Object);
        
        //Act
        sut.Build(DataType.Document, "", SearchStrategyType.SignedSearch);
        
        //Assert
        inputDataFolderReaderFactoryMock.Verify(x => x.MakeDataFolderReader(DataType.Document), Times.Once);
        invertedIndexMock.Verify(x => x.AddDataListToMap(It.IsAny<IEnumerable<ISearchable>>(), extractorMock.Object), Times.Once);
        inputSearchStrategyFactoryMock.Verify(x => x.MakeSearchController(SearchStrategyType.SignedSearch), Times.Once);
    }

    [Fact]
    public void Search_ShouldCallCorrectMethods_Whenever()
    {
        //Arrange
        var inputDataFolderReaderFactoryMock = new Mock<IDataFolderReaderFactory>();
        var invertedIndexMock = new Mock<IInvertedIndex>();
        var inputSearchStrategyFactoryMock = new Mock<ISearchStrategyFactory>();
        var textEditorMock = new Mock<ITextEditor>();
        var extractorMock = new Mock<IExtractor>();
        
        var queryMock = new Mock<IQuery>();
        var searchControllerMock = new Mock<ISearchController>();
        
        var sut = new SearchInitializer(inputDataFolderReaderFactoryMock.Object, invertedIndexMock.Object, inputSearchStrategyFactoryMock.Object, textEditorMock.Object, extractorMock.Object);
        sut.SearchController = searchControllerMock.Object;
        
        //Act
        sut.Search(queryMock.Object);

        //Assert
        searchControllerMock.Verify(x => x.SearchWithQuery(queryMock.Object, invertedIndexMock.Object), Times.Once);
    }
}