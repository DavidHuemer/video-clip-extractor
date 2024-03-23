using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Managers.VideoCacheManager;
using VideoClipExtractor.Core.Services.VideoProvider.CachedVideosService;
using VideoClipExtractor.Core.Services.VideoProvider.RemainingVideosService;
using VideoClipExtractor.Core.Services.VideoProvider.RequestedVideosService;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoProvider;

/// <summary>
///     Provides videos from the repository.
/// </summary>
[Transient]
public class VideoProvider : IVideoProvider
{
    #region Private Fields

    /// <summary>
    ///     The maximum size of the cache.
    /// </summary>
    public const int CacheSize = 10;

    #endregion

    private readonly ICachedVideosService _cachedVideosService;
    private readonly IVideoCacheManager _cacheManager;
    private readonly IRemainingVideosService _remainingVideosService;
    private readonly IRequestedVideosService _requestedVideosService;

    public VideoProvider(IDependencyProvider provider)
    {
        _cacheManager = provider.GetDependency<IVideoCacheManager>();
        _cacheManager.VideoCached += OnFileCached;
        _cacheManager.Error += OnError;

        _remainingVideosService = provider.GetDependency<IRemainingVideosService>();
        _requestedVideosService = provider.GetDependency<IRequestedVideosService>();
        _cachedVideosService = provider.GetDependency<ICachedVideosService>();
    }

    #region Events

    public event Action<VideoViewModel>? VideoAdded;

    #endregion

    public void Setup(Project project, IVideoRepository repository)
    {
        _cacheManager.Setup(project, repository);
        _remainingVideosService.Setup(project);
        _requestedVideosService.Setup(project);

        var workingSourceVideos = project.WorkingVideos.Select(video => video.SourceVideo).ToList();
        _cacheManager.CacheVideos(workingSourceVideos);

        for (var i = 0; i < _remainingVideosService.AllowedCacheSize; i++) ExtendCache();
    }

    public void Next()
    {
        ExtendCache();

        if (_cachedVideosService.IsVideoCached)
        {
            ProvideVideo();
        }
        else
        {
            _requestedVideosService.Request();
        }
    }

    private void OnError(object? sender, EventArgs e)
    {
        _requestedVideosService.ErrorOccured();
        ExtendCache();
    }

    private void OnFileCached(CachedVideo video)
    {
        _cachedVideosService.Add(video);

        if (!_requestedVideosService.IsVideoRequested) return;
        ProvideVideo();
        ExtendCache();
    }

    /// <summary>
    ///     Extends the cache by one video.
    /// </summary>
    private void ExtendCache()
    {
        if (!_remainingVideosService.IsVideoRemaining) return;

        var sourceVideo = _remainingVideosService.GetNextVideo();
        _cacheManager.CacheVideo(sourceVideo);
    }

    private void ProvideVideo()
    {
        var cachedVideo = _cachedVideosService.GetNextCachedVideo();
        VideoAdded?.Invoke(_requestedVideosService.GetNextRequestedVideo(cachedVideo));
    }
}