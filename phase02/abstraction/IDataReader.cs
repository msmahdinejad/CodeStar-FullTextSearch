namespace phase02;
interface IDataReader
{
    void RaedFolder();
    Task<string> RaedData(string path);

}