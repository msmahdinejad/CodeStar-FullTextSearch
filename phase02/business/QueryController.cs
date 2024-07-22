namespace phase02;

public class QueryController
{
    public Query MyQuery {get; set; }

    public HashSet<string> UnSignedWords { get; set; }
    public HashSet<string> PositiveWords { get; set; }
    public HashSet<string> NegativeWords { get; set; }
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
            if(word[0] == '+')
            {
                PositiveWords.Add(word);
            }
        }
    }
    public void FindNegativeWords(){
        foreach (var word in SplitedText)
        {
            if(word[0] == '-')
            {
                NegativeWords.Add(word);
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