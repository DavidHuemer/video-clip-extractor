using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.Cleanup.CacheCleanup;

[Singleton]
public class CacheCleanupService(IDependencyProvider provider) : ICacheCleanupService
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();

    public void CleanUpCachedVideo(VideoViewModel video)
    {
        if (!_fileService.FileExists(video.LocalPath)) return;

        _fileService.DeleteFile(video.LocalPath);
    }
}