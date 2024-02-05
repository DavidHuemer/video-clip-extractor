using BaseUI.Data;

namespace BaseUI.Services.RecentlyOpened;

/// <summary>
///     Responsible for managing recently opened files.
/// </summary>
public interface IRecentlyOpenedFilesService
{
    /// <summary>
    ///     Adds a file to the list of recently opened files.
    /// </summary>
    /// <param name="path">The path to the file that should be added</param>
    void AddFile(string path);

    /// <summary>
    ///     Returns a list of recently opened files.
    /// </summary>
    /// <returns>List of recently opened files</returns>
    List<RecentlyOpenedFileInfo> GetRecentlyOpenedFiles();
}