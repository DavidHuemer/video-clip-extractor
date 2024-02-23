using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionNames;

/// <summary>
/// Responsible for generating the names of the extractions.
/// </summary>
public interface IExtractionNameService
{
    /// <summary>
    /// Returns the name of the image at the given index.
    /// </summary>
    /// <param name="video">The video in which the extraction is located</param>
    /// <param name="imageExtraction">The extraction for which the path should be returned</param>
    /// <returns>The name of the image extraction</returns>
    string GetImagePath(VideoViewModel video, ImageExtraction imageExtraction);

    /// <summary>
    /// Returns the name of the video at the given index.
    /// </summary>
    /// <param name="video">The video in which the extraction is located</param>
    /// <param name="videoExtraction">The extraction for which the path should be returned</param>
    /// <returns>The name of the video extraction</returns>
    string GetVideoPath(VideoViewModel video, VideoExtraction videoExtraction);
}