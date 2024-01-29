using VideoClipExtractor.Data.Basics.Events;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;

/// <summary>
/// Responsible for crawling videos from a source.
/// </summary>
public interface IVideoCrawler
{
    /// <summary>
    /// Crawls videos from a source.
    /// </summary>
    void CrawlVideos();

    #region Events

    /// <summary>
    /// Occurs when a video is added.
    /// </summary>
    event EventHandler<SourceVideoEventArgs>? VideoAdded;

    /// <summary>
    /// Occurs when the crawler has finished.
    /// </summary>
    event EventHandler? CrawlerFinished;

    /// <summary>
    /// Occurs when an exception is thrown.
    /// </summary>
    event EventHandler<ExceptionEventArgs>? ExceptionThrown;

    #endregion
}