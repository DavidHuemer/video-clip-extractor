using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ImageExtractions;

public interface IImageExtractionService
{
    Task Extract(VideoViewModel video, ImageExtraction imageExtraction);
}