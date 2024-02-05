using System.IO;
using System.Text.Json;
using BaseUI.Data;
using BaseUI.Services.Basics.Time;
using BaseUI.Services.DependencyInjection;

namespace BaseUI.Services.RecentlyOpened;

public abstract class BaseRecentlyOpenedFilesService(IDependencyProvider provider) : IRecentlyOpenedFilesService
{
    protected abstract string ProjectDirectory { get; }

    public void AddFile(string path)
    {
        // Load recently opened files
        var recentlyOpenedFiles = GetRecentlyOpenedFiles();
        var existingFile = recentlyOpenedFiles.FirstOrDefault(file => file.Path == path);

        // Check if file is already in list
        if (existingFile != null)
        {
            // If so, update last opened time
            existingFile.LastOpened = provider.GetDependency<ITimeService>().GetCurrentTime();
        }
        else
        {
            // If not, add to list
            var recentlyOpenedFile = CreateRecentlyOpenedFileInfo(path);
            recentlyOpenedFiles.Add(recentlyOpenedFile);
        }

        // Store recently opened files
        StoreRecentlyOpenedFiles(recentlyOpenedFiles);
    }

    public List<RecentlyOpenedFileInfo> GetRecentlyOpenedFiles()
    {
        if (!File.Exists(RecentlyOpenedFilesPath))
        {
            return [];
        }

        var recentlyOpenedFilesJson = File.ReadAllText(RecentlyOpenedFilesPath);
        return JsonSerializer.Deserialize<List<RecentlyOpenedFileInfo>>(recentlyOpenedFilesJson) ??
               [];
    }

    private RecentlyOpenedFileInfo CreateRecentlyOpenedFileInfo(string path)
    {
        return new RecentlyOpenedFileInfo
        {
            Path = path,
            LastOpened = provider.GetDependency<ITimeService>().GetCurrentTime(),
        };
    }

    #region IO Operations

    private void StoreRecentlyOpenedFiles(IEnumerable<RecentlyOpenedFileInfo> recentlyOpenedFileInfos)
    {
        var recentlyOpenedFilesJson = JsonSerializer.Serialize(recentlyOpenedFileInfos);

        if (!Directory.Exists(ProjectDirectoryPath)) Directory.CreateDirectory(ProjectDirectoryPath);

        // Write to file (can also create file)
        File.WriteAllText(RecentlyOpenedFilesPath, recentlyOpenedFilesJson);
    }

    private string RecentlyOpenedFilesPath => Path.Combine(ProjectDirectoryPath, "recentlyOpened.json");

    private string ProjectDirectoryPath => Path.Combine(
        Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), ProjectDirectory);

    #endregion
}