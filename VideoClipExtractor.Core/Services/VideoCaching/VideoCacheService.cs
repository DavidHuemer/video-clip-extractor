using BaseUI.Exceptions.Basics;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.VideoCaching.CacheProcessor;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoCaching;

[Transient]
public class VideoCacheService : IVideoCacheService
{
    private readonly ICacheProcessor _cacheProcessor;

    public VideoCacheService(IDependencyProvider provider)
    {
        _cacheProcessor = provider.GetDependency<ICacheProcessor>();
        _cacheProcessor.OnResultProcessed += OnVideoCached;
        _cacheProcessor.OnErrorOccurred += OnErrorOccured;
    }

    public event Action<CachedVideo>? VideoCached;
    public event Action<Exception>? Error;

    public void Setup(Project project, IVideoRepository repository)
    {
        _cacheProcessor.Setup(project, repository);
    }

    public void CacheVideo(SourceVideo video)
    {
        if (!_cacheProcessor.IsSetup)
            throw new NotSetupException(nameof(VideoCacheService), nameof(Setup));

        _cacheProcessor.AddVideo(video);
    }

    private void OnVideoCached(CachedVideo video) =>
        VideoCached?.Invoke(video);

    private void OnErrorOccured(Exception exception) =>
        Error?.Invoke(exception);
}