namespace BaseUI.Services.FileServices;

public interface IFileExplorer
{
    string GetSaveFilePath(string filter, string defaultExtension);
}