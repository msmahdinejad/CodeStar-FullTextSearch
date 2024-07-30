namespace TestProject.QueryModel;
using phase02.QueryModel;

public class QueryTests
{
    [Fact]
    public void SplittedText_EmptyText_ReturnsEmptyArray()
    {
        //Arrange
        var query = new Query("");
        //Act
        var result = query.SplitedText;
        //Assert
        Assert.Empty(result);
    }

    [Fact]
    public void SplittedText_InputText_ReturnsUpperTextArray()
    {
        //Arrange
        var query = new Query("aBc air ...");
        //Act
        var result = query.SplitedText;
        //Assert
        Assert.Contains("ABC", result);
        Assert.Contains("AIR", result);
        Assert.Contains("...", result);
    }
    
    [Fact]
    public void SplittedText_InputText_ReturnsSplittedArray()
    {
        //Arrange
        var query = new Query("aBc l.;;d kjshd ksjdl");
        //Act
        var result = query.SplitedText;
        //Assert
        Assert.Equal(4, result.Length);
    }
}