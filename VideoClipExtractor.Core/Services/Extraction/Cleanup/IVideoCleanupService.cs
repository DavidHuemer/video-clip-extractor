using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.Cleanup;

public interface IVideoCleanupService
{
    VideoExtractionResult CleanupVideo(VideoViewModel video,
        List<ExtractionResult> extractionResults);
}