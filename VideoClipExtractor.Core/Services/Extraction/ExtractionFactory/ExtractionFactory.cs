using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.ExtractionFactory;

[Transient]
public class ExtractionFactory : IExtractionFactory
{
    public IImageExtraction GetImageExtraction(VideoPosition position) => new ImageExtraction(position);

    public IVideoExtraction GetVideoExtraction(VideoPosition begin, VideoViewModel video)
    {
        return new VideoExtraction(begin, new VideoPosition(begin.Frame + 30));
    }
}