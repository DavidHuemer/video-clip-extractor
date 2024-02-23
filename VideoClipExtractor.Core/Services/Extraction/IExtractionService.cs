using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction;

/// <summary>
/// Responsible for extracting images and videos from a video file.
/// </summary>
public interface IExtractionService
{
    event EventHandler? StartImageExtractions;

    /// <summary>
    /// Extracts images and videos from the given video.
    /// </summary>
    /// <param name="video">The video out of which the images and videos should be extracted</param>
    Task Extract(VideoViewModel video);
}