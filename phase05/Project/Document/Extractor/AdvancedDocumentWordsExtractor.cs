namespace phase02.Document.Extractor;

public class AdvancedDocumentWordsExtractor : IExtractor
{
    public IEnumerable<string> GetKey(ISearchable data)
    {
        var words = data.GetWords();
        var result = new List<string>();
        for (int i = 1; i <= 4; i++)
        {
            for (int j = 0; j <= words.Count() - i; j++)
            {
                string sentence = words.ElementAt(j);
                for (int k = 1; k < i; k++)
                {
                    sentence = sentence + " " + words.ElementAt(j+k);
                }
                result.Add(sentence);
            }
        }

        return result;
    }
}