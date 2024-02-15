using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class VideoExtractionPartViewModel(VideoPosition videoPosition) : BaseExtractionViewModel
{
    public VideoPosition VideoPosition { get; set; } = videoPosition;
}