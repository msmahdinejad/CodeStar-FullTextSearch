using FullTextsearch.QueryManager.WordFinder;

namespace FullTextSearch.Test.QueryManager.WordFinder;

public class PositiveWordFinderTests
{
    public static IEnumerable<object[]> FindWordsTestData()
    {
        yield return new object[]
        {
            new[] { "+apple", "+banana", "+cherry", "-date" },
            new HashSet<string> { "apple", "banana", "cherry" }
        };

        yield return new object[]
        {
            new[] { "+apple", "-banana", "+cherry", "date" },
            new HashSet<string> { "apple", "cherry" }
        };

        yield return new object[]
        {
            new[] { "-apple", "-banana", "-cherry" },
            new HashSet<string>()
        };

        yield return new object[]
        {
            new string[] {},
            new HashSet<string>()
        };

        yield return new object[]
        {
            new[] { "+", "+apple", "+banana", "-cherry" },
            new HashSet<string> { "", "apple", "banana" }
        };
    }

    [Theory]
    [MemberData(nameof(FindWordsTestData))]
    public void FindWords_InputQuery_ReturnsPositiveWords(string[] input, HashSet<string> expected)
    {
        //Arrange
        var unSignedWordFinder = new PositiveWordFinder();  
        
        // Act
        var result = unSignedWordFinder.FindWords(input);

        // Assert
        Assert.Equal(expected, result);
    }
}