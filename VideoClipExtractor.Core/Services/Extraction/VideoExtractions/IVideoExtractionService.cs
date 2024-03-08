using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.VideoExtractions;

public interface IVideoExtractionService
{
    Task<ExtractionResult> Extract(VideoViewModel video, VideoExtraction videoExtraction);
}