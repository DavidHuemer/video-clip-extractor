using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Data.VideoRepos;

/// <summary>
///     Repository where the videos are stored.
/// </summary>
public interface IVideoRepository
{
    /// <summary>
    ///     Connects to the repository.
    /// </summary>
    void Connect();

    /// <summary>
    ///     Copies the video given by the sourceVideoPath to the cacheVideoPath
    /// </summary>
    /// <param name="sourceVideoPath">The path where the video is located in the repo</param>
    /// <param name="cacheVideoPath">The path where the video should be cached</param>
    void CopyFileByPath(string sourceVideoPath, string cacheVideoPath);

    /// <summary>
    ///     Returns the videos from the repository.
    /// </summary>
    IEnumerable<SourceVideo> GetFiles();

    /// <summary>
    /// Removes the video from the repository by the given path.
    /// </summary>
    /// <param name="videoSourcePath">The path to the video that should be removed</param>
    void RemoveVideoByPath(string videoSourcePath);
}