using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;

/// <summary>
///     Responsible for crawling videos from a source.
/// </summary>
public interface IVideoCrawler
{
    /// <summary>
    ///     Crawls videos from a source.
    /// </summary>
    Task CrawlVideos();

    #region Events

    event Action<List<SourceVideo>> VideosAdded;

    #endregion
}