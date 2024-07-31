using phase02.resources;

namespace phase02.Exceptions;

public class InvalidSearchStrategy : Exception
{
    public InvalidSearchStrategy() : base(Resources.InvalidSearchStrategyMessage)
    {
    }
}