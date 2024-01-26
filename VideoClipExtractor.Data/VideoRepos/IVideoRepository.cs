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
}