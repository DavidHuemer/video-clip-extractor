namespace BaseUI.Services.FileServices;

public interface IFileService
{
    bool FileExists(string path);

    bool DirectoryExists(string path);

    string GetTmpFolder();
    void CreateDirectory(string extractionFolderPath);
}