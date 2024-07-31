using phase02.resources;

namespace phase02.Exceptions;

public class InvalidFolderPath : Exception
{
    public InvalidFolderPath() : base(Resources.InvalidFolderPathMessage,  new FileNotFoundException())
    {
    }
}