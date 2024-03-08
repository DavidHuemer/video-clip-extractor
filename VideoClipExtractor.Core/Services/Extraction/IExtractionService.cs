using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction;

/// <summary>
/// Responsible for extracting images and videos from a video file.
/// </summary>
public interface IExtractionService
{
    /// <summary>
    /// Extracts images and videos from a video file and returns the video extraction result.
    /// </summary>
    /// <param name="video">The video that should be extracted</param>
    /// <returns>The result of the video extraction</returns>
    Task<VideoExtractionResult> Extract(VideoViewModel video);
}