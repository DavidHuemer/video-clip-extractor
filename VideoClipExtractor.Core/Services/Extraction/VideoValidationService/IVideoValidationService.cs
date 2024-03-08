using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.VideoValidationService;

/// <summary>
/// Responsible for validating a video for extraction
/// </summary>
public interface IVideoValidationService
{
    /// <summary>
    /// Validates that a video is ready for extraction.
    /// <para></para>
    /// When a video is not ready for extraction, an exception is thrown.
    /// </summary>
    /// <param name="video">The video that is validated</param>
    void ValidateVideoForExtraction(VideoViewModel video);
}