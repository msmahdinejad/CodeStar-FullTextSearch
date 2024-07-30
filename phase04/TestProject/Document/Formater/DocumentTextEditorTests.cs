using phase02.Document.Formater;

namespace TestProject.Document.Formater;

public class DocumentTextEditorTests
{
    [Theory]
    [InlineData("", new string[] { })]
    [InlineData("Hello World", new string[] { "HELLO", "WORLD" })]
    [InlineData("Hello, World!", new string[] { "HELLO", "WORLD" })]
    [InlineData("Hello\tWorld\nGoodbye\rWorld", new string[] { "HELLO", "WORLD", "GOODBYE", "WORLD" })]
    [InlineData("Hello, World! @2021 #testing", new string[] { "HELLO", "WORLD", "2021", "TESTING" })]
    [InlineData("Testing with symbols *&^%$#@!", new string[] { "TESTING", "WITH", "SYMBOLS" })]
    [InlineData("Numbers 123 and 456", new string[] { "NUMBERS", "123", "AND", "456" })]
    [InlineData("   Leading and trailing spaces   ", new string[] { "LEADING", "AND", "TRAILING", "SPACES" })]
    [InlineData("Multiple   spaces between   words", new string[] { "MULTIPLE", "SPACES", "BETWEEN", "WORDS" })]
    public void TextSplitter_ShouldSplitTextCorrectly(string input, string[] expected)
    {
        // Arrange
        var testEditor = new DocumentTextEditor();
        
        // Act
        var result = testEditor.TextSplitter(input);

        // Assert
        Assert.Equal(expected, result);
    }
}