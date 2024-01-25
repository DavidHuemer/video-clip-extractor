namespace BaseUI.Services.FileServices;

public interface IFileExplorer
{
    /// <summary>
    ///     Opens a save-file explorer and returns the path of the selected file
    /// </summary>
    /// <param name="filter">The filter that describes how the saving file should look like</param>
    /// <returns>The path to the selected file</returns>
    string GetSaveFilePath(string filter);

    /// <summary>
    /// Opens a explorer that allows the user to select a directory
    /// </summary>
    /// <returns>The selected path to the directory</returns>
    string GetBrowseDirectoryPath();
}