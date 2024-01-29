using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Data.VideoRepos;

/// <summary>
/// Repository where the videos are stored.
/// </summary>
public interface IVideoRepository
{
    /// <summary>
    /// Connects to the repository.
    /// </summary>
    void Connect();

    /// <summary>
    /// Returns the videos from the repository.
    /// </summary>
    IEnumerable<SourceVideo> GetFiles();
}