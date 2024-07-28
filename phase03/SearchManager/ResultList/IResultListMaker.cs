namespace phase02;

public interface IResultListMaker
{
    HashSet<ISearchable> MakeResultList(HashSet<string> keyList, IInvertedIndex myInvertedIndex);
}