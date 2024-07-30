using NSubstitute;
using phase02.Document.Formater;

namespace TestProject.Document;

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
        var expectedKeys = new[] { "test" };
        _textEditor.TextSplitter(_text).Returns(expectedKeys);
        
        // Act
        var result = _sut.GetKey();

        // Assert
        Assert.Equal(expectedKeys, result);
    }
    
    [Fact]
    public void GetKey_ShouldCallTextSplitter_Whenever()
    {
        // Arrange
        
        // Act
       _sut.GetKey();

        // Assert
        _textEditor.Received(1).TextSplitter(_text);
    }
    
    [Fact]
    public void GetValue_ShouldReturnDocumentName_Whenever()
    {
        // Arrange
        
        // Act
        var result = _sut.GetValue();

        // Assert
        Assert.Equal(result, _name);
    }
}