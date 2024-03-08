using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;
using VideoClipExtractor.Data.Exceptions.VideoRepositoryExceptions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.Cleanup.VideoRepoCleanup;

[Singleton]
public class VideoRepoCleanupService(IDependencyProvider provider) : IVideoRepoCleanupService
{
    private readonly IVideoRepositoryManager
        _videoRepositoryManager = provider.GetDependency<IVideoRepositoryManager>();

    public void CleanupVideo(VideoViewModel video)
    {
        var videoRepository = _videoRepositoryManager.VideoRepository;

        if (videoRepository == null)
            throw new VideoRepositoryNotSetException();

        videoRepository.RemoveVideoByPath(video.SourcePath);
    }
}