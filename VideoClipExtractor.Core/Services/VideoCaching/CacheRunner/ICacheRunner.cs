using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;

namespace VideoClipExtractor.Core.Services.VideoCaching.CacheRunner;

/// <summary>
/// Runs the caching process
/// </summary>
public interface ICacheRunner
{
    void StoreVideo(string sourcePath);

    void Setup(Project project, IVideoRepository repository);
}