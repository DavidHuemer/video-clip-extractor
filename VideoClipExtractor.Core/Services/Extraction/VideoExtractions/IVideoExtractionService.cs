using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.VideoExtractions;

public interface IVideoExtractionService
{
    Task Extract(VideoViewModel video, VideoExtraction videoExtraction);
}