
namespace phase02;

public class SearchController
{
    public HashSet<string> UnSignedDocumentsResult;
    public HashSet<string> PositiveDocumentsResult;
    public HashSet<string> NegativeDocumentsResult;
    public HashSet<string> FinalResult;
    public Search MySearch {get; set;}
    public InvertedIndexController MyInvertedIndex {get; set;}
    public SearchController(HashSet<string> unSignedWords, HashSet<string> positiveWords, HashSet<string> negativeWords)
    {
        this.MySearch = new Search(unSignedWords, positiveWords, negativeWords);
        this.MyInvertedIndex = InvertedIndexController.Instance;
        UnSignedDocumentsResult = new HashSet<string>();
        PositiveDocumentsResult = new HashSet<string>();
        NegativeDocumentsResult = new HashSet<string>();
        FinalResult = new HashSet<string>();    
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
        try
        {
            return new HashSet<string>(MyInvertedIndex.MyInvertedIndex.Words[wordUpperCase]);
        }
        catch (KeyNotFoundException)
        { 
            return new HashSet<string>();
        }
    }
    public HashSet<string> SearchWithQuery()
    {
        UnSignedSearch();
        PositiveSearch();
        NegativeSearch();
        if (UnSignedDocumentsResult.Count > 0)
        {
            UnSignedDocumentsResult.ExceptWith(NegativeDocumentsResult);
            if (MySearch.PositiveWords.Count != 0)
            {
                UnSignedDocumentsResult.IntersectWith(PositiveDocumentsResult);
            }
            FinalResult = UnSignedDocumentsResult;
        }
        else if (PositiveDocumentsResult.Count > 0)
        {
            PositiveDocumentsResult.ExceptWith(NegativeDocumentsResult);
            FinalResult = PositiveDocumentsResult;
        }
        else if (NegativeDocumentsResult.Count > 0)
        {
            FinalResult = new HashSet<string>(MyInvertedIndex.AllDocuments);
            FinalResult.ExceptWith(NegativeDocumentsResult);
        }
        return FinalResult;
    }
}