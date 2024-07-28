namespace phase02;
public interface ISearchStrategy
{
    HashSet<ISearchable> SearchWithQuery(Query query);
}