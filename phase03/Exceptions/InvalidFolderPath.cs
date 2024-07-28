using phase02.resources;

namespace phase02.Exceptions;

public class InvalidFolderPath : Exception
{
    public InvalidFolderPath(string message) : base(message)
    {
    }

    public InvalidFolderPath() : base(Resource1.InvalidFolderPathMessage)
    {
    }
}