using phase02.QueryManager.WordFinder;

namespace FullTextSearch.Test.QueryManager.WordFinder;

public class UnSignedWordFinderTests
{
    public static IEnumerable<object[]> FindWordsTestData()
    {
        yield return new object[]
        {
            new[] { "apple", "banana", "cherry" },
            new HashSet<string> { "apple", "banana", "cherry" }
        };

        yield return new object[]
        {
            new[] { "+apple", "-banana", "cherry" },
            new HashSet<string> { "cherry" }
        };

        yield return new object[]
        {
            new[] { "+apple", "-banana", "+cherry" },
            new HashSet<string>()
        };

        yield return new object[]
        {
            new string[] {},
            new HashSet<string>()
        };

        yield return new object[]
        {
            new[] { "+", "-", "+apple", "-banana" },
            new HashSet<string>()
        };
    }

    [Theory]
    [MemberData(nameof(FindWordsTestData))]
    public void FindWords_InputQuery_ReturnsUnSignedWords(string[] input, HashSet<string> expected)
    {
        //Arrange
        var unSignedWordFinder = new UnsignedWordFinder();  
        
        // Act
        var result = unSignedWordFinder.FindWords(input);

        // Assert
        Assert.Equal(expected, result);
    }
}