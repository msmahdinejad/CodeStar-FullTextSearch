using phase02.QueryModel;

namespace phase02.QueryManager;

public interface IQueryBuilder
{
    void Build(IQuery query);
}