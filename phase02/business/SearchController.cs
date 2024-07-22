
namespace phase02;

public class SearchController
{
    public HashSet<string> UnSignedDocumentsResult;
    public HashSet<string> PositiveDocumentsResult;
    public HashSet<string> NegativeDocumentsResult;
    public Search MySearch {get; set;}
    public InvertedIndexController MyInvertedIndex {get; set;}
    public SearchController(HashSet<string> unSignedWords, HashSet<string> positiveWords, HashSet<string> negativeWords)
    {
        this.MySearch = new Search(unSignedWords, positiveWords, negativeWords);
        this.MyInvertedIndex = InvertedIndexController.Instance;
    }
    public void UnSignedSearch()
    {
        if(MySearch.UnSignedWords.Count >= 1)
        {
            UnSignedDocumentsResult = Search(MySearch.UnSignedWords.First());
            foreach(var word in MySearch.UnSignedWords)
            {
                UnSignedDocumentsResult.IntersectWith(Search(word));
            }
        }
    }
    public void PositiveSearch()
    {
        if(MySearch.PositiveWords.Count >= 1)
        {
            PositiveDocumentsResult = Search(MySearch.PositiveWords.First());
            foreach(var word in MySearch.PositiveWords)
            {
                PositiveDocumentsResult.UnionWith(Search(word));
            }
        }
    }
    public void NegativeSearch()
    {
        if(MySearch.NegativeWords.Count >= 1)
        {
            NegativeDocumentsResult = Search(MySearch.NegativeWords.First());
            foreach(var word in MySearch.NegativeWords)
            {
                NegativeDocumentsResult.UnionWith(Search(word));
            }
        }
    }

    public HashSet<string> Search(string word)
    {
        var wordUpperCase = word.ToUpper();
        return MyInvertedIndex.MyInvertedIndex.Words[wordUpperCase];
    }
    public HashSet<string> SearchWithQuery()
    {
        UnSignedSearch();
        PositiveSearch();
        NegativeSearch();
        UnSignedDocumentsResult.ExceptWith(NegativeDocumentsResult);
        UnSignedDocumentsResult.IntersectWith(PositiveDocumentsResult);
        return UnSignedDocumentsResult;
    }
}