
namespace phase02;

public class SearchController
{
    InvertedIndexController myInvertedIndex;
    public SearchController()
    {
        this.myInvertedIndex = InvertedIndexController.Instance;
    }

    public HashSet<string> Search(string word)
    {
        var wordUpperCase = word.ToUpper();
        return myInvertedIndex.invertedeIndex.Words[wordUpperCase];
    }
}