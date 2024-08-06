using FullTextsearch.resources;

namespace FullTextsearch.Exceptions;

public class InvalidSearchStrategy : Exception
{
    public InvalidSearchStrategy() : base(Resources.InvalidSearchStrategyMessage)
    {
    }
}