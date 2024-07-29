namespace phase02;

public interface IWordFinder
{
    HashSet<string> FindWords(Query query);
}