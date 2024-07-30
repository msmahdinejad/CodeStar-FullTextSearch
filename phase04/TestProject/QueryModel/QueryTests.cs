namespace TestProject.QueryModel;
using phase02.QueryModel;

public class QueryTests
{
    [Fact]
    public void SplittedText_NullText_ReturnsNullArray()
    {
        //Arrange
        var query = new Query("");
        //Act
        
        //Assert
        Assert.Empty(query.SplitedText);
    }
}