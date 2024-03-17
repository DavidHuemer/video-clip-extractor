using System.IO;
using BaseUI.Exceptions.Basics;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;

namespace VideoClipExtractor.Core.Services.VideoCaching.CacheRunner;

[Transient]
public class CacheRunner(IDependencyProvider provider) : ICacheRunner
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();

    private VideoCacheInformation? _cacheInformation;

    private bool IsSetup => _cacheInformation != null;

    public void StoreVideo(string sourcePath)
    {
        if (!IsSetup)
            throw new NotSetupException(nameof(CacheRunner), nameof(StoreVideo));

        // Check if the video already exists
        var fileName = Path.GetFileName(sourcePath);

        var localPath = Path.Combine(_cacheInformation!.LocalCachePath, fileName);
        var videoExists = _fileService.FileExists(localPath);

        if (videoExists)
        {
            // If it does, update the video (delete the old one and store the new one)
            _fileService.DeleteFile(localPath);
        }

        _cacheInformation!.Repository.CopyFileByPath(sourcePath, localPath);
    }

    public void Setup(Project project, IVideoRepository repository) =>
        _cacheInformation = new VideoCacheInformation(repository, project.ImageDirectory);
}