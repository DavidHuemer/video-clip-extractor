using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoCaching;

/// <summary>
/// Caches videos
/// </summary>
public interface IVideoCacheService
{
    /// <summary>
    ///     Is invoked when a video has been cached
    /// </summary>
    public event Action<CachedVideo>? VideoCached;

    /// <summary>
    /// Is invoked when an error occurs
    /// </summary>
    public event Action<Exception>? Error;

    /// <summary>
    ///     Sets up the cache service
    /// </summary>
    /// <param name="project"></param>
    /// <param name="repository">The <see cref="IVideoRepository" /> from which the videos should be fetched</param>
    public void Setup(Project project, IVideoRepository repository);

    /// <summary>
    ///     Caches the given video
    /// </summary>
    /// <param name="video">The video that should be cached</param>
    public void CacheVideo(SourceVideo video);
}