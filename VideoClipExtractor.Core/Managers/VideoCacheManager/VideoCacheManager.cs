using BaseUI.Exceptions.Basics;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.VideoCaching;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Managers.VideoCacheManager;

[Singleton]
public class VideoCacheManager(IDependencyProvider provider) : IVideoCacheManager
{
    private IVideoCacheService? _cacheService;
    public event Action<CachedVideo>? VideoCached;
    public event EventHandler? Error;

    public void Setup(Project project, IVideoRepository repository)
    {
        if (_cacheService != null)
        {
            _cacheService.VideoCached -= OnVideoCached;
            _cacheService.Error -= OnError;
        }

        _cacheService = provider.GetDependency<IVideoCacheService>();
        _cacheService.Setup(project, repository);
        _cacheService.VideoCached += OnVideoCached;
        _cacheService.Error += OnError;
    }

    public void CacheVideos(List<SourceVideo> videos)
    {
        if (_cacheService == null)
            throw new NotSetupException(nameof(VideoCacheManager), nameof(Setup));

        videos.ForEach(video => _cacheService.CacheVideo(video));
    }

    public void CacheVideo(SourceVideo video)
    {
        if (_cacheService == null)
            throw new NotSetupException(nameof(VideoCacheManager), nameof(Setup));

        _cacheService.CacheVideo(video);
    }

    private void OnVideoCached(CachedVideo video) =>
        VideoCached?.Invoke(video);

    private void OnError(Exception exception) =>
        Error?.Invoke(this, EventArgs.Empty);
}