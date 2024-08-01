using System.ComponentModel;
using phase02.Document;
using phase02.Document.Formater;
using phase02.SearchManager;
using phase02.SearchManager.SignedSearchManager;

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
        HashSet<ISearchable> unsignedResult,
        HashSet<ISearchable> positiveResult,
        HashSet<ISearchable> negativeResult,
        HashSet<ISearchable> allResults,
        HashSet<ISearchable> expectedResult)
    {
        // Act
        var result = _sut.GetResult(unsignedResult, positiveResult, negativeResult, allResults);

        // Assert
        Assert.Equal(expectedResult, result);
    }

    public static IEnumerable<object[]> GetTestData()
    {
        var doc1 = new phase02.Document.Document("1", "", new DocumentTextEditor());
        var doc2 = new phase02.Document.Document("2", "", new DocumentTextEditor());
        var doc3 = new phase02.Document.Document("3", "", new DocumentTextEditor());
        
        yield return new object[]
        {
            new HashSet<ISearchable> { doc1, doc2 },
            new HashSet<ISearchable> { doc2, doc3 },
            new HashSet<ISearchable>(),
            new HashSet<ISearchable> { doc1, doc2, doc3 },
            new HashSet<ISearchable> { doc2 }
        };

        yield return new object[]
        {
            new HashSet<ISearchable>(),
            new HashSet<ISearchable> { doc1, doc2 },
            new HashSet<ISearchable> { doc2 },
            new HashSet<ISearchable> { doc1, doc2 },
            new HashSet<ISearchable> { doc1 }
        };

        yield return new object[]
        {
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>(),
            new HashSet<ISearchable> { doc1 },
            new HashSet<ISearchable> { doc1, doc2 },
            new HashSet<ISearchable> { doc2 }
        };

        yield return new object[]
        {
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>(),
            new HashSet<ISearchable>()
        };
    }
}