using FullTextsearch.resources;

namespace FullTextsearch.Exceptions;

public class InvalidFolderPath : Exception
{
    public InvalidFolderPath() : base(Resources.InvalidFolderPathMessage,  new FileNotFoundException())
    {
    }
}