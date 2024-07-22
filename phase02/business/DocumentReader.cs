using System.Reflection;

namespace phase02;
public class DocumentReader : IDataReader
{
    public void RaedFolder()
    {
        var folderPath="Resource.path";//change it:)

        var files=Directory.GetFiles(folderPath);
        foreach (var file in files)
        {
            var data=RaedData(file);
            var name=Path.GetFileName(file);
            // add func:)))))
        }
    }

    public async Task<string> RaedData(string path)
    {
        return await File.ReadAllTextAsync(path);
    }


}