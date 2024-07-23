
namespace phase02;

public class SearchController
{
    public HashSet<string> UnSignedDocumentsResult { get; set; }
    public HashSet<string> PositiveDocumentsResult { get; set; }
    public HashSet<string> NegativeDocumentsResult { get; set; }
    public HashSet<string> FinalResult { get; set; }
    public Search MySearch { get; init; }
    public InvertedIndexController MyInvertedIndex { get; init; }
    public SearchController(HashSet<string> unSignedWords, HashSet<string> positiveWords, HashSet<string> negativeWords)
    {
        MySearch = new Search { UnSignedWords = unSignedWords, PositiveWords = positiveWords, NegativeWords = negativeWords };
        MyInvertedIndex = InvertedIndexController.Instance;
        UnSignedDocumentsResult = new HashSet<string>();
        PositiveDocumentsResult = new HashSet<string>();
        NegativeDocumentsResult = new HashSet<string>();
        FinalResult = new HashSet<string>();
    }
    private void UnSignedSearch()
    {
        if (MySearch.UnSignedWords.Count >= 1)
        {
            UnSignedDocumentsResult = Search(MySearch.UnSignedWords.First());
            foreach (var word in MySearch.UnSignedWords)
            {
                UnSignedDocumentsResult.IntersectWith(Search(word));
            }
        }
    }
    private void PositiveSearch()
    {
        if (MySearch.PositiveWords.Count >= 1)
        {
            PositiveDocumentsResult = Search(MySearch.PositiveWords.First());
            foreach (var word in MySearch.PositiveWords)
            {
                PositiveDocumentsResult.UnionWith(Search(word));
            }
        }
    }
    private void NegativeSearch()
    {
        if (MySearch.NegativeWords.Count >= 1)
        {
            NegativeDocumentsResult = Search(MySearch.NegativeWords.First());
            foreach (var word in MySearch.NegativeWords)
            {
                NegativeDocumentsResult.UnionWith(Search(word));
            }
        }
    }

    private HashSet<string> Search(string word)
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