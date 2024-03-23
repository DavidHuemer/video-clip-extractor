using BaseUI.Handler;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoCaching.CacheProcessor;

/// <summary>
/// Processes the caching of videos
/// </summary>
public interface ICacheProcessor : IProcessing<CachedVideo>
{
    bool IsSetup { get; }
    void AddVideo(SourceVideo video);
    void Setup(Project project, IVideoRepository repository);
}