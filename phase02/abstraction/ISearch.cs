namespace phase02;
public interface ISearch
{
    HashSet<ISearchable> SearchWithQuery(Query query);
}