using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Extractions.Results;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.FileExtractionService;

public interface IFileExtractionService
{
    Task<ExtractionResult> Extract(VideoViewModel video, IExtraction extraction);
}