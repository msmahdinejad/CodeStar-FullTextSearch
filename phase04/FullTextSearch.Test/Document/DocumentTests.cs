using NSubstitute;
using phase02.Document.Formater;

namespace FullTextSearch.Test.Document;

public class DocumentTests
{
    private readonly phase02.Document.Document _sut;
    private readonly ITextEditor _textEditor;
    private readonly string _text = "testDocText";
    private readonly string _name = "testDocName";
    
    public DocumentTests()
    {
        _textEditor = Substitute.For<ITextEditor>();
        _sut = new phase02.Document.Document(_name, _text, _textEditor);
    }
    
    [Fact]
    public void GetKey_ShouldReturnExpectedKeys_WhenCallTextSplitter()
    {
        // Arrange
        var text = "testDocText";
        var expectedKeys = new[] { "test" };
        _textEditor.TextSplitter(text).Returns(expectedKeys);
        
        // Act
        var result = _sut.GetKey();

        // Assert
        Assert.Equal(expectedKeys, result);
    }
    
    [Fact]
    public void GetKey_ShouldCallTextSplitter_Whenever()
    {
        // Arrange
        var text = "testDocText";

        // Act
       _sut.GetKey();

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