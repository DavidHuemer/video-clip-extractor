using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ImageExtractions;

public interface IImageExtractionService
{
    Task<ExtractionResult> Extract(VideoViewModel video, ImageExtraction imageExtraction);
}