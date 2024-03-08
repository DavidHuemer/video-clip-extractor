using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionRunnerService;

public interface IExtractionRunnerService
{
    Task<List<ExtractionResult>> ExtractVideo(VideoViewModel video);
}