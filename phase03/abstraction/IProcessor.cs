namespace phase02;

public interface IProcessor
{
    void Build();
    HashSet<ISearchable> Search(Query query);
}