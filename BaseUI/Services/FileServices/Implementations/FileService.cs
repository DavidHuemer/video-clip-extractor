using System.IO;
using JetBrains.Annotations;

namespace BaseUI.Services.FileServices.Implementations;

[UsedImplicitly]
public class FileService : IFileService
{
    public bool FileExists(string path) => File.Exists(path);
    public bool DirectoryExists(string path) => Directory.Exists(path);
    public string GetTmpFolder() => Path.GetTempPath();
    public void CreateDirectory(string extractionFolderPath) => Directory.CreateDirectory(extractionFolderPath);

    public int GetFileSize(string path)
    {
        var fileInfo = new FileInfo(path);
        return (int)fileInfo.Length;
    }

    public void DeleteFile(string path) => File.Delete(path);
}