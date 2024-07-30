using phase02.resources;

namespace phase02.Exceptions;

public class InvalidSearchStrategy : Exception
{
    public InvalidSearchStrategy(string message) : base(message)
    {
    }

    public InvalidSearchStrategy() : base(Resource1.InvalidSearchStrategyMessage)
    {
    }
}