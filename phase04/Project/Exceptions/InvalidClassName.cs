using phase02.resources;

namespace phase02.Exceptions;

public class InvalidClassName : Exception
{
    public InvalidClassName(string message) : base(message)
    {
    }

    public InvalidClassName() : base(Resource1.InvalidClassNameMessage)
    {
    }
}