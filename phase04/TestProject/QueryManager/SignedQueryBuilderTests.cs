using Moq;
using phase02.QueryManager;
using phase02.QueryManager.WordFinder;
using phase02.QueryModel;

namespace TestProject.QueryManager;

using Xunit;
using System.Collections.Generic;

public class SignedQueryBuilderTests
{
    public static IEnumerable<object[]> GetTestData()
    {
        yield return new object[]
        {
            new string[] { "+apple", "-banana", "cherry", "+date", "-fig" },
            new HashSet<string> { "cherry" }, // UnSignedWords
            new HashSet<string> { "apple", "date" }, // PositiveWords
            new HashSet<string> { "banana", "fig" } // NegativeWords
        };

        yield return new object[]
        {
            new string[] { "+apple", "+banana", "+cherry" },
            new HashSet<string> { }, // UnSignedWords
            new HashSet<string> { "apple", "banana", "cherry" }, // PositiveWords
            new HashSet<string> { } // NegativeWords
        };

        yield return new object[]
        {
            new string[] { "-apple", "-banana", "-cherry" },
            new HashSet<string> { }, // UnSignedWords
            new HashSet<string> { }, // PositiveWords
            new HashSet<string> { "apple", "banana", "cherry" } // NegativeWords
        };

        yield return new object[]
        {
            new string[] { "cherry", "date", "fig" },
            new HashSet<string> { "cherry", "date", "fig" }, // UnSignedWords
            new HashSet<string> { }, // PositiveWords
            new HashSet<string> { } // NegativeWords
        };

        yield return new object[]
        {
            new string[] { },
            new HashSet<string> { }, // UnSignedWords
            new HashSet<string> { }, // PositiveWords
            new HashSet<string> { } // NegativeWords
        };

        yield return new object[]
        {
            new string[] { "-", "+apple", "+banana", "-cherry", "+", "-" },
            new HashSet<string> { }, // UnSignedWords
            new HashSet<string> { "apple", "banana" , ""}, // PositiveWords
            new HashSet<string> { "cherry", "" } // NegativeWords
        };
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void Build_InputQuery_CategorizesWordsCorrectly(string[] splittedText, HashSet<string> expectedUnSignedWords, HashSet<string> expectedPositiveWords, HashSet<string> expectedNegativeWords)
    {
        // Arrange
        var builder = new SignedQueryBuilder();
        var mockQuery = new Mock<IQuery>();
        mockQuery.SetupGet(q => q.SplitedText).Returns(splittedText);
        
        // Act
        builder.Build(mockQuery.Object);

        // Assert
        Assert.Equal(expectedUnSignedWords, builder.UnSignedWords);
        Assert.Equal(expectedPositiveWords, builder.PositiveWords);
        Assert.Equal(expectedNegativeWords, builder.NegativeWords);
    }
}