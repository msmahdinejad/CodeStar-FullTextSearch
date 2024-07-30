using phase02.QueryModel;

namespace phase02.QueryManager.WordFinder;

public interface IWordFinder
{
    HashSet<string> FindWords(Query query);
}