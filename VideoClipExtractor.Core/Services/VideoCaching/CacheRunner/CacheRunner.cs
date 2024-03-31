using System.IO;
using BaseUI.Exceptions.Basics;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using FFMpeg.Wrapper.MpegInfo;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoCaching.CacheRunner;

[Transient]
public class CacheRunner(IDependencyProvider provider) : ICacheRunner
{
    private readonly IFileService _fileService = provider.GetDependency<IFileService>();
    private readonly IMpegInfo _mpegInfo = provider.GetDependency<IMpegInfo>();

    private VideoCacheInformation? _cacheInformation;

    public bool IsSetup => _cacheInformation != null;

    public async Task<CachedVideo> StoreVideo(SourceVideo sourceVideo)
    {
        if (!IsSetup)
            throw new NotSetupException(nameof(CacheRunner), nameof(StoreVideo));

        // Check if the video already exists
        var localPath = Path.Combine(_cacheInformation!.LocalCachePath, sourceVideo.FullName);
        var videoExists = _fileService.FileExists(localPath);

        if (videoExists)
        {
            // If it does, update the video (delete the old one and store the new one)
            _fileService.DeleteFile(localPath);
        }

        _cacheInformation!.Repository.CopyFileByPath(sourceVideo.Path, localPath);
        var videoInfo = await _mpegInfo.GetVideoInfoAsync(localPath);
        return new CachedVideo(sourceVideo, localPath, videoInfo);
    }

    public void Setup(Project project, IVideoRepository repository) =>
        _cacheInformation = new VideoCacheInformation(repository, project.ImageDirectory);
}