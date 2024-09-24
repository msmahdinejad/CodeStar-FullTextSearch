using Moq;
using FullTextsearch.Document;
using FullTextsearch.Document.Abstraction;
using FullTextsearch.Document.Extractor;
using FullTextsearch.Document.Extractor.Abstraction;
using FullTextsearch.Factory.SearchFactory;
using FullTextsearch.InvertedIndex;
using FullTextsearch.SearchManager;

namespace FullTextSearch.Test.InvertedIndex;

public class InvertedIndexControllerTests
{
    private readonly InvertedIndexController _sut;

    public InvertedIndexControllerTests()
    {
        _sut = new InvertedIndexController();
    }

    [Fact]
    public void AddDataToMap_ShouldAddCorrectly_WhenDataIsOk()
    {
        //Arrange
        var keys = new List<string> { "a" };
        var docName = "test";
        var dataMock = new Mock<ISearchable>();
        var extractorMock = new Mock<IExtractor>();
        extractorMock.Setup(x => x.GetKey(dataMock.Object)).Returns(keys);
        dataMock.Setup(x => x.GetValue()).Returns(docName);
        
        //Act
        _sut.AddDataToMap(dataMock.Object, extractorMock.Object);
        var result = _sut.GetValue("a");

        //Assert
        Assert.Single(result);
        Assert.Equal(result.First().GetValue(), docName);
    }
    
    [Fact]
    public void AddDataListToMap_ShouldAddCorrectly_WhenDataListIsOk()
    {
        //Arrange
        var extractorMock = new Mock<IExtractor>();
        
        
        var keys = new List<string> { "a" };
        var docName = "test";
        var dataMock = new Mock<ISearchable>();
        dataMock.Setup(x => x.GetValue()).Returns(docName);
        extractorMock.Setup(x => x.GetKey(dataMock.Object)).Returns(keys);
        
        var keys2 = new List<string> { "b" };
        var docName2 = "test2";
        var dataMock2 = new Mock<ISearchable>();
        dataMock2.Setup(x => x.GetValue()).Returns(docName2);
        extractorMock.Setup(x => x.GetKey(dataMock2.Object)).Returns(keys2);
        
        var keys3 = new List<string> { "a" };
        var docName3 = "test3";
        var dataMock3 = new Mock<ISearchable>();
        dataMock3.Setup(x => x.GetValue()).Returns(docName3);
        extractorMock.Setup(x => x.GetKey(dataMock3.Object)).Returns(keys3);

        //Act
        _sut.AddDataListToMap(new List<ISearchable>{dataMock.Object,dataMock2.Object,dataMock3.Object}, extractorMock.Object);
        var result = _sut.GetValue("a");
        var result2 = _sut.GetValue("b");

        //Assert
        Assert.Equal(2,result.Count);
        Assert.Single(result2);
        Assert.Equal(result.First().GetValue(), docName);
        Assert.Equal(result.ElementAt(1).GetValue(), docName3);
        Assert.Equal(result2.First().GetValue(), docName2);
    }
    
    [Fact]
    public void GetValue_ShouldReturnsEmptyList_WhenKeyDoesNotExist()
    {
        //Arrange
        var keys = new List<string> { "a" };
        var docName = "test";
        var dataMock = new Mock<ISearchable>();
        dataMock.Setup(x => x.GetValue()).Returns(docName);
        var extractorMock = new Mock<IExtractor>();
        extractorMock.Setup(x => x.GetKey(dataMock.Object)).Returns(keys);
        
        
        //Act
        _sut.AddDataToMap(dataMock.Object, extractorMock.Object);
        var result = _sut.GetValue("b");

        //Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetAllValue_ShouldReturnsAllValue_When()
    {
        //Arrange
        var extractorMock = new Mock<IExtractor>();
        
        
        var keys = new List<string> { "a" };
        var docName = "test";
        var dataMock = new Mock<ISearchable>();
        dataMock.Setup(x => x.GetValue()).Returns(docName);
        extractorMock.Setup(x => x.GetKey(dataMock.Object)).Returns(keys);
        
        var keys2 = new List<string> { "b" };
        var docName2 = "test2";
        var dataMock2 = new Mock<ISearchable>();
        dataMock2.Setup(x => x.GetValue()).Returns(docName2);
        extractorMock.Setup(x => x.GetKey(dataMock2.Object)).Returns(keys);
        
        var keys3 = new List<string> { "a" };
        var docName3 = "test3";
        var dataMock3 = new Mock<ISearchable>();
        dataMock3.Setup(x => x.GetValue()).Returns(docName3);
        extractorMock.Setup(x => x.GetKey(dataMock3.Object)).Returns(keys);

        var expectedResult = new HashSet<ISearchable> { dataMock.Object, dataMock2.Object, dataMock3.Object };
        
        //Act
        _sut.AddDataListToMap(new List<ISearchable>{dataMock.Object,dataMock2.Object,dataMock3.Object}, extractorMock.Object);
        var result = _sut.GetAllValue();

        //Assert
        Assert.Equal(result,expectedResult);
    }
    
    
}