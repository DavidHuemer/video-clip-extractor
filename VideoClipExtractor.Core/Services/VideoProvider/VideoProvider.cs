using BaseUI.Exceptions.Basics;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Services.VideoCaching;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Services.VideoProvider;

/// <summary>
///     Provides videos from the repository.
/// </summary>
public class VideoProvider : IVideoProvider
{
    public VideoProvider(IDependencyProvider provider)
    {
        _cacheHandler = provider.GetDependency<IVideoCacheService>();
        _cacheHandler.VideoCached += OnFileCached;
    }

    #region Events

    public event EventHandler<VideoEventArgs>? VideoAdded;

    #endregion

    public void Setup(Project project, IVideoRepository repository)
    {
        _cacheHandler.Setup(repository, project.ImageDirectory);
        InitVideos(project);
        _isSetup = true;
    }

    public void Next()
    {
        if (!_isSetup) throw new NotSetupException(nameof(VideoProvider), nameof(Setup));

        ExtendCache();

        if (_cache.Count > 0)
            ProvideVideo();
        else
            _requestedVideos++;
    }

    private void InitVideos(Project project)
    {
        var sourceVideosList = project.Videos.ToList().OrderByDescending(sourceVideo => sourceVideo.Size)
            .ThenBy(sourceVideo => sourceVideo.FullName)
            .SkipWhile(sourceVideo => sourceVideo.Checked)
            .ToList();

        _remainingSourceVideos = new Queue<SourceVideo>(sourceVideosList);
        _requestedVideos = 1;

        var cacheCount = Math.Min(CacheSize, _remainingSourceVideos.Count);
        for (var i = 0; i < cacheCount; i++) ExtendCache();
    }

    /// <summary>
    ///     Extends the cache by one video.
    ///     It takes the next video from the <see cref="_remainingSourceVideos" /> and caches it.
    /// </summary>
    private void ExtendCache()
    {
        if (_remainingSourceVideos.Count <= 0) return;

        var sourceVideo = _remainingSourceVideos.Dequeue();
        _cacheHandler.CacheVideo(sourceVideo);
    }

    private void ProvideVideo()
    {
        var cachedVideo = _cache.Dequeue();
        var video = new Video(cachedVideo);
        VideoAdded?.Invoke(this, new VideoEventArgs(video));
    }

    private void OnFileCached(object? sender, VideoCachedEventArgs e)
    {
        _cache.Enqueue(e.CachedVideo);

        if (_requestedVideos <= 0) return;
        _requestedVideos--;
        ProvideVideo();
    }

    #region Private Fields

    /// <summary>
    ///     The maximum size of the cache.
    /// </summary>
    private const int CacheSize = 10;

    private readonly IVideoCacheService _cacheHandler;

    /// <summary>
    ///     Contains the cached videos.
    /// </summary>
    private readonly Queue<CachedVideo> _cache = new();

    /// <summary>
    ///     Contains the remaining videos that are not done and not cached.
    /// </summary>
    private Queue<SourceVideo> _remainingSourceVideos = new();

    /// <summary>
    ///     The number of videos that are currently requested.
    ///     In other words: The number of videos that are expected.
    /// </summary>
    private int _requestedVideos;

    private bool _isSetup;

    #endregion
}