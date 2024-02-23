using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.FileExtractionService;

public interface IFileExtractionService
{
    Task Extract(VideoViewModel video, IExtraction extraction);
}