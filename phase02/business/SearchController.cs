
namespace phase02;

public class SearchController
{
    public HashSet<string> search(string word)
    {
        InvertedIndexController myInvertedIndex = InvertedIndexController.Instance;
        var wordUpperCase = word.ToUpper();
        return myInvertedIndex.invertedeIndex.Words[wordUpperCase];
    }
}