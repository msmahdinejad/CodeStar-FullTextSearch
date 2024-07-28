namespace phase02;
public interface ISearchable
{
    IEnumerable<string> GetKey();
    string GetValue();
}