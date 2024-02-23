namespace VideoClipExtractor.Tests.Basics.Extensions;

public class TestFolder
{
    public TestFolder(string folderName)
    {
        FolderName = folderName;
        FolderPath = Path.Combine(Path.GetTempPath(), FolderName);

        ClearFolder();
    }

    public string FolderName { get; set; }

    public string FolderPath { get; set; }

    private void ClearFolder()
    {
        if (Directory.Exists(FolderPath))
            Directory.Delete(FolderPath, true);

        Directory.CreateDirectory(FolderPath);
    }

    public string GetFilePath(string fileName)
    {
        return Path.Combine(FolderPath, fileName);
    }

    public string GetFolderPath(string folderName)
    {
        return Path.Combine(FolderPath, folderName);
    }

    public void RemoveFolder()
    {
        if (Directory.Exists(FolderPath))
            Directory.Delete(FolderPath, true);
    }
}