using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Services.VideoCaching;

public interface IVideoCacheService
{
    /// <summary>
    /// Is called when a video has been cached
    /// </summary>
    public event EventHandler<VideoCachedEventArgs>? VideoCached;

    /// <summary>
    /// Sets up the cache service
    /// </summary>
    /// <param name="repository">The <see cref="IVideoRepository"/> from which the videos should be fetched</param>
    /// <param name="cachingDirectory">The path to which the videos should be cached</param>
    public void Setup(IVideoRepository repository, string cachingDirectory);

    /// <summary>
    /// Caches the given video
    /// </summary>
    /// <param name="video">The video that should be cached</param>
    public void CacheVideo(SourceVideo video);
}