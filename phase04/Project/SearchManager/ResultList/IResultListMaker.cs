using phase02.Document;
using phase02.InvertedIndex;

namespace phase02.SearchManager.ResultList;

public interface IResultListMaker
{
    HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex);
}