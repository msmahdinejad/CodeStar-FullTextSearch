using FullTextsearch.resources;

namespace FullTextsearch.Exceptions;

public class InvalidClassName : Exception
{
    public InvalidClassName() : base(Resources.InvalidClassNameMessage, new Exception())
    {
    }
}