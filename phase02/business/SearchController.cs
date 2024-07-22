
namespace phase02;

public class SearchController
{
    public HashSet<string> search(string word)
    {
        InvertedIndexController myInvertedIndex = InvertedIndexController.Instance;
        return myInvertedIndex.invertedeIndex.Words[word];
    }
}