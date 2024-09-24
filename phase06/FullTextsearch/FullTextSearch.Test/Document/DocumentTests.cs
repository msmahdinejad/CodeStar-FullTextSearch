using NSubstitute;
using FullTextsearch.Document.Formater;
using FullTextsearch.Document.Formater.Abstraction;

namespace FullTextSearch.Test.Document;

public class DocumentTests
{
    private readonly FullTextsearch.Document.Document _sut;
    private readonly ITextEditor _textEditor;
    private readonly string _text = "testDocText";
    private readonly string _name = "testDocName";
    
    public DocumentTests()
    {
        _textEditor = Substitute.For<ITextEditor>();
        _sut = new FullTextsearch.Document.Document(_name, _text, _textEditor);
    }
    
    [Fact]
    public void GetKey_ShouldReturnExpectedKeys_WhenCallTextSplitter()
    {
        // Arrange
        var text = "testDocText";
        var expectedKeys = new[] { "test" };
        _textEditor.TextSplitter(text).Returns(expectedKeys);
        
        // Act
        var result = _sut.GetWords();

        // Assert
        Assert.Equal(expectedKeys, result);
    }
    
    [Fact]
    public void GetKey_ShouldCallTextSplitter_Whenever()
    {
        // Arrange
        var text = "testDocText";

        // Act
       _sut.GetWords();

        // Assert
        _textEditor.Received(1).TextSplitter(text);
    }
    
    [Fact]
    public void GetValue_ShouldReturnDocumentName_Whenever()
    {
        // Arrange
        var name = "testDocName";

        // Act
        var result = _sut.GetValue();

        // Assert
        Assert.Equal(result, name);
    }
}