namespace BaseUI.Services.FileServices;

public interface IFileService
{
    /// <summary>
    /// Returns true if the file exists
    /// </summary>
    /// <param name="path">The path to the file</param>
    /// <returns>If the file exists</returns>
    bool FileExists(string path);

    /// <summary>
    /// Returns true if the directory exists
    /// </summary>
    /// <param name="path">The path to the directory</param>
    /// <returns>If the directory exists</returns>
    bool DirectoryExists(string path);

    /// <summary>
    /// Returns the path to the temporary folder
    /// </summary>
    /// <returns>Path to the temporary folder</returns>
    string GetTmpFolder();

    /// <summary>
    /// Creates a directory
    /// </summary>
    /// <param name="extractionFolderPath">The path of the directory that should be created</param>
    void CreateDirectory(string extractionFolderPath);

    /// <summary>
    /// Returns the size of the file
    /// </summary>
    /// <param name="path">The path to the file</param>
    /// <returns>The size of the file in bytes</returns>
    int GetFileSize(string path);

    /// <summary>
    /// Deletes the file
    /// </summary>
    /// <param name="path">The path to the file</param>
    void DeleteFile(string path);
}