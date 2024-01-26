using Microsoft.Win32;

namespace BaseUI.Services.FileServices.Implementations;

/// <summary>
///     Basic implementation of the <see cref="IFileExplorer" /> interface
/// </summary>
// ReSharper disable once ClassNeverInstantiated.Global
public class FileExplorer : IFileExplorer
{
    public string GetSaveFilePath(string filter)
    {
        var saveFileDialog = new SaveFileDialog
        {
            Filter = filter,
        };
        var result = saveFileDialog.ShowDialog();
        return result == true ? saveFileDialog.FileName : string.Empty;
    }

    public string GetBrowseDirectoryPath()
    {
        var folderBrowserDialog = new OpenFolderDialog();
        var result = folderBrowserDialog.ShowDialog();
        return result == true ? folderBrowserDialog.FolderName : string.Empty;
    }

    public string GetOpenFilePath(string filter)
    {
        var openFileDialog = new OpenFileDialog
        {
            Filter = filter,
        };
        var result = openFileDialog.ShowDialog();
        return result == true ? openFileDialog.FileName : string.Empty;
    }
}