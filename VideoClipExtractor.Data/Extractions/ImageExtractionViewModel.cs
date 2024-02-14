using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class ImageExtractionViewModel : BaseViewModel
{
    public ImageExtractionViewModel(VideoPosition position)
    {
        VideoPosition = position;
    }

    public VideoPosition VideoPosition { get; set; }
}