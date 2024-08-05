using Moq;
using phase02.Document;
using phase02.Factory.SearchFactory;
using phase02.InvertedIndex;
using phase02.SearchManager;

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
        dataMock.Setup(x => x.GetKey()).Returns(keys);
        dataMock.Setup(x => x.GetValue()).Returns(docName);
        
        //Act
        _sut.AddDataToMap(dataMock.Object);
        var result = _sut.GetValue("a");

        //Assert
        Assert.Single(result);
        Assert.Equal(result.First().GetValue(), docName);
    }
    
    [Fact]
    public void AddDataListToMap_ShouldAddCorrectly_WhenDataListIsOk()
    {
        //Arrange
        var keys = new List<string> { "a" };
        var docName = "test";
        var dataMock = new Mock<ISearchable>();
        dataMock.Setup(x => x.GetKey()).Returns(keys);
        dataMock.Setup(x => x.GetValue()).Returns(docName);
        
        var keys2 = new List<string> { "b" };
        var docName2 = "test2";
        var dataMock2 = new Mock<ISearchable>();
        dataMock2.Setup(x => x.GetKey()).Returns(keys2);
        dataMock2.Setup(x => x.GetValue()).Returns(docName2);
        
        var keys3 = new List<string> { "a" };
        var docName3 = "test3";
        var dataMock3 = new Mock<ISearchable>();
        dataMock3.Setup(x => x.GetKey()).Returns(keys3);
        dataMock3.Setup(x => x.GetValue()).Returns(docName3);

        //Act
        _sut.AddDataListToMap(new List<ISearchable>{dataMock.Object,dataMock2.Object,dataMock3.Object});
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
        dataMock.Setup(x => x.GetKey()).Returns(keys);
        dataMock.Setup(x => x.GetValue()).Returns(docName);
        
        //Act
        _sut.AddDataToMap(dataMock.Object);
        var result = _sut.GetValue("b");

        //Assert
        Assert.Empty(result);
    }
    
    [Fact]
    public void GetAllValue_ShouldReturnsAllValue_When()
    {
        //Arrange
        var keys = new List<string> { "a" };
        var docName = "test";
        var dataMock = new Mock<ISearchable>();
        dataMock.Setup(x => x.GetKey()).Returns(keys);
        dataMock.Setup(x => x.GetValue()).Returns(docName);
        
        var keys2 = new List<string> { "b" };
        var docName2 = "test2";
        var dataMock2 = new Mock<ISearchable>();
        dataMock2.Setup(x => x.GetKey()).Returns(keys2);
        dataMock2.Setup(x => x.GetValue()).Returns(docName2);
        
        var keys3 = new List<string> { "a" };
        var docName3 = "test3";
        var dataMock3 = new Mock<ISearchable>();
        dataMock3.Setup(x => x.GetKey()).Returns(keys3);
        dataMock3.Setup(x => x.GetValue()).Returns(docName3);

        var expectedResult = new HashSet<ISearchable> { dataMock.Object, dataMock2.Object, dataMock3.Object };
        
        //Act
        _sut.AddDataListToMap(new List<ISearchable>{dataMock.Object,dataMock2.Object,dataMock3.Object});
        var result = _sut.GetAllValue();

        //Assert
        Assert.Equal(result,expectedResult);
    }
    
    
}