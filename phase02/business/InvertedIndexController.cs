using Microsoft.VisualBasic;

namespace phase02;
public class InvertedIndexController
{
    private static InvertedIndexController _instance;
    public InvertedeIndex invertedeIndex { get; set; }
    private InvertedIndexController()
    {
        this.invertedeIndex = new InvertedeIndex();
    }
    public static InvertedIndexController Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new InvertedIndexController();
            }
            return _instance;
        }
    }
    public void AddTextToMap(string name, string text)
    {
        string[] wordList = text.TextSpliter();

        foreach (var item in wordList)
        {
            if (!invertedeIndex.Words.ContainsKey(item))
            {
                invertedeIndex.Words[item] = [name];
            }
            else
            {
                invertedeIndex.Words[item].Add(name);
            }
        }
    }
}