namespace phase02;
public interface ISearch
{
    HashSet<ISearchable> MakeResultList(HashSet<string> keyList);
}