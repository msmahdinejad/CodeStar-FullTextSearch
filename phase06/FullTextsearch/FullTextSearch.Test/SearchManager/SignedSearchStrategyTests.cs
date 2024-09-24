using System.ComponentModel;
using FullTextsearch.Document;
using FullTextsearch.Document.Formater;
using FullTextsearch.SearchManager;
using FullTextsearch.SearchManager.SignedSearchManager;

namespace FullTextSearch.Test.SearchManager;

public class SignedSearchStrategyTests
{
    private readonly SignedSearchStrategy _sut;

    public SignedSearchStrategyTests()
    {
        _sut = new SignedSearchStrategy();
    }

    [Theory]
    [MemberData(nameof(GetTestData))]
    public void GetResult_ShouldReturnExpectedResult_Whenever(
        HashSet<string> unSignedWords,
        HashSet<string> positiveWords, 
        HashSet<string> negativeWords,
        HashSet<ISearchable> unsignedResult,
        HashSet<ISearchable> positiveResult,
        HashSet<ISearchable> negativeResult,
        HashSet<ISearchable> allResults,
        HashSet<ISearchable> expectedResult)
    {
        // Act
        var result = _sut.GetResult(unSignedWords, positiveWords, negativeWords, unsignedResult, positiveResult, negativeResult, allResults);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    public static IEnumerable<object[]> GetTestData()
    {
        var doc1 = new FullTextsearch.Document.Document("1", "", new DocumentTextEditor());
        var doc2 = new FullTextsearch.Document.Document("2", "", new DocumentTextEditor());
        var doc3 = new FullTextsearch.Document.Document("3", "", new DocumentTextEditor());
        
        yield return new object[]
        {
            new HashSet<string>() {"1"},
            new HashSet<string>() {"1"},
            new HashSet<string>() {},
            new HashSet<ISearchable> { doc1, doc2 },
            new HashSet<ISearchable> { doc2, doc3 },
            new HashSet<ISearchable>(),
            new HashSet<ISearchable> { doc1, doc2, doc3 },
            new HashSet<ISearchable> { doc2 }
        };

        yield return new object[]
        {
            new HashSet<string>() {},
            new HashSet<string>() {"1"},
            new HashSet<string>() {"1"},
            new HashSet<ISearchable>(),
            new HashSet<ISearchable> { doc1, doc2 },
            new HashSet<ISearchable> { doc2 },
            new HashSet<ISearchable> { doc1, doc2 },
            new HashSet<ISearchable> { doc1 }
        };

        yield return new object[]
        {
            new HashSet<string>() {},
            new HashSet<string>() {},
            new HashSet<string>() {"1"},
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>(),
            new HashSet<ISearchable> { doc1 },
            new HashSet<ISearchable> { doc1 },
            new HashSet<ISearchable> { }
        };

        yield return new object[]
        {
            new HashSet<string>() {},
            new HashSet<string>() {},
            new HashSet<string>() {},
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>()
        };
    }
}