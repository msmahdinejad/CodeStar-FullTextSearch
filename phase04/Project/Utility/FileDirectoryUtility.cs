using phase02.Utility.Abstractions;

namespace phase02.Utility;

public class FileDirectoryUtility : IFileDirectoryUtility
{
    public string[] GetFiles(string path)
    {
        return Directory.GetFiles(path);
    }

    public string GetFileName(string path)
    {
        return Path.GetFileName(path);
    }

    public string ReadAllText(string path)
    {
        return File.ReadAllText(path);
    }
}