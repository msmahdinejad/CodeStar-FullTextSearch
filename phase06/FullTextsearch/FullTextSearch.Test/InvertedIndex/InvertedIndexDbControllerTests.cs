using FullTextsearch.Context;
using FullTextsearch.Document;
using FullTextsearch.Document.Extractor;
using FullTextsearch.InvertedIndex;
using FullTextsearch.Model;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace FullTextSearch.Test.InvertedIndex;

public class InvertedIndexDbControllerTests
{
    private readonly ApplicationDbContext _context;
    private readonly InvertedIndexDbController _sut;

    public InvertedIndexDbControllerTests()
    {
        // Create an in-memory database for testing
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        _context = new ApplicationDbContext(options);
        _sut = new InvertedIndexDbController(_context);
    }

    [Fact]
    public void AddDataToMap_ShouldAddNewRecord_WhenKeyDoesNotExist()
    {
        // Arrange
        var keys = new List<string> { "a" };
        var dataMock = new Mock<ISearchable>();
        var extractorMock = new Mock<IExtractor>();
        extractorMock.Setup(x => x.GetKey(dataMock.Object)).Returns(keys);
        dataMock.Setup(x => x.GetValue()).Returns("testValue");

        // Act
        _sut.AddDataToMap(dataMock.Object, extractorMock.Object);

        // Assert
        var record = _context.InvertedIndexMap.FirstOrDefault(x => x.Key == "a");
        Assert.NotNull(record);
        Assert.Contains("testValue", record.Values);
    }

    [Fact]
    public void AddDataToMap_ShouldAddValueToExistingRecord_WhenKeyExists()
    {
        // Arrange
        var existingRecord = new InvertedIndexRecord { Key = "a", Values = new[] { "existingValue" } };
        _context.InvertedIndexMap.Add(existingRecord);
        _context.SaveChanges();

        var keys = new List<string> { "a" };
        var dataMock = new Mock<ISearchable>();
        var extractorMock = new Mock<IExtractor>();
        extractorMock.Setup(x => x.GetKey(dataMock.Object)).Returns(keys);
        dataMock.Setup(x => x.GetValue()).Returns("testValue");

        // Act
        _sut.AddDataToMap(dataMock.Object, extractorMock.Object);

        // Assert
        var updatedRecord = _context.InvertedIndexMap.First(x => x.Key == "a");
        Assert.Contains("testValue", updatedRecord.Values);
    }

    [Fact]
    public void GetValue_ShouldReturnHashSet_WhenKeyExists()
    {
        // Arrange
        var record = new InvertedIndexRecord { Key = "a", Values = new[] { "value1", "value2" } };
        _context.InvertedIndexMap.Add(record);
        _context.SaveChanges();

        // Act
        var result = _sut.GetValue("a");

        // Assert
        Assert.Equal(2, result.Count);
    }

    [Fact]
    public void GetValue_ShouldReturnEmptyHashSet_WhenKeyDoesNotExist()
    {
        // Act
        var result = _sut.GetValue("b");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public void GetAllValue_ShouldReturnAllValues_WhenCalled()
    {
        // Arrange
        var record1 = new InvertedIndexRecord { Key = "a", Values = new[] { "value1" } };
        var record2 = new InvertedIndexRecord { Key = "b", Values = new[] { "value2" } };
        _context.InvertedIndexMap.AddRange(record1, record2);
        _context.SaveChanges();

        // Act
        var result = _sut.GetAllValue();

        // Assert
        Assert.Equal(2, result.Count);
    }
}