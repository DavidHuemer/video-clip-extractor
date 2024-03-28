using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Managers.VideoCacheManager;

/// <summary>
/// Manages the caching service
/// </summary>
public interface IVideoCacheManager
{
    /// <summary>
    /// Invoked when a video is cached
    /// </summary>
    event Action<CachedVideo>? VideoCached;

    /// <summary>
    /// Invoked when an error occurs
    /// </summary>
    event EventHandler Error;

    void Setup(Project project, IVideoRepository repository);

    void CacheVideos(List<SourceVideo> videos);

    void CacheVideo(SourceVideo video);
}