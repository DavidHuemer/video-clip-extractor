using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionFactory;

public interface IExtractionFactory
{
    IImageExtraction GetImageExtraction(VideoPosition position1);

    IVideoExtraction GetVideoExtraction(VideoPosition begin, VideoViewModel video);
}