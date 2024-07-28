namespace phase02;
public interface ISearchResultList
{
    HashSet<ISearchable> MakeResultList(HashSet<string> keyList, InvertedIndexController myInvertedIndex);
}