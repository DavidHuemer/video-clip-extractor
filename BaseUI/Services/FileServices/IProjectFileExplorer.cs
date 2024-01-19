namespace BaseUI.Services.FileServices;

public interface IProjectFileExplorer
{
    /// <summary>
    ///     Returns the path where the project file should be saved.
    /// </summary>
    /// <returns>The path where the project file should be saved</returns>
    string GetSaveProjectFilePath();
}