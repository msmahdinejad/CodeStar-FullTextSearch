using FullTextsearch.QueryModel;

namespace FullTextSearch.Test.QueryModel;

public class AdvancedQueryTests
{
    [Fact]
    public void SplittedText_EmptyText_ReturnsEmptyArray()
    {
        //Arrange
        var query = new AdvancedQuery("");
        //Act
        var result = query.SplitedText;
        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public void SplittedText_InputText_ReturnsUpperTextArray()
    {
        //Arrange
        var query = new AdvancedQuery("aBc air -\"test\"");
        //Act
        var result = query.SplitedText;
        //Assert
        Assert.Contains("ABC", result);
        Assert.Contains("AIR", result);
        Assert.Contains("-TEST", result);
    }
    
    [Fact]
    public void SplittedText_InputText_ReturnsSplittedArray()
    {
        //Arrange
        var query = new AdvancedQuery("get +illness +disease -cough -\"star academy\"");
        //Act
        var result = query.SplitedText;
        //Assert
        Assert.Equal(5, result.Length);
    }
}