using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class ImageExtractionViewModel(VideoPosition position) : BaseExtractionViewModel
{
    public VideoPosition VideoPosition { get; set; } = position;
}