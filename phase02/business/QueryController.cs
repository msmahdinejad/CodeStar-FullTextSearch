namespace phase02;

public class QueryController
{
    public Query MyQuery {get; init; }

    public HashSet<string> UnSignedWords { get; init; }
    public HashSet<string> PositiveWords { get; init; }
    public HashSet<string> NegativeWords { get; init; }
    public string[] SplitedText {get; set;}
    public QueryController(string text)
    {
        this.MyQuery = new Query(text);
        this.UnSignedWords = new HashSet<string>();
        this.PositiveWords = new HashSet<string>();
        this.NegativeWords = new HashSet<string>();
    }
    public void TextSpliter()
    {
        this.SplitedText=MyQuery.Text.Split(new[] {' '}, StringSplitOptions.RemoveEmptyEntries);
    }
    public void FindUnSignWords(){
        foreach (var word in SplitedText)
        {
            if(word[0] != '+' && word[0] != '-')
            {
                UnSignedWords.Add(word);
            }
        }
    }
    public void FindPositiveWords(){
        foreach (var word in SplitedText)
        {
            if (word[0] == '+')
            {
                PositiveWords.Add(word.Substring(1));
            }
        }
    }
    public void FindNegativeWords(){
        foreach (var word in SplitedText)
        {
            if(word[0] == '-')
            {
                NegativeWords.Add(word.Substring(1));
            }
        }
    }
    public HashSet<string> RunQuery()
    {
        TextSpliter();
        FindUnSignWords();
        FindPositiveWords();
        FindNegativeWords();
        var MySearchController = new SearchController(UnSignedWords, PositiveWords, NegativeWords);
        return MySearchController.SearchWithQuery();
    }



}