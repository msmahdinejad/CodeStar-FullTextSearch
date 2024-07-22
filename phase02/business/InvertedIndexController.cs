using Microsoft.VisualBasic;

namespace phase02;
public class InvertedIndexController
{
    public InvertedeIndex invertedeIndex { get; set; }
    public InvertedIndexController()
    {
        this.invertedeIndex = new InvertedeIndex();
    }

    public void AddTextToMap(string name, string text)
    {
        string[] wordList = text.FixText();
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