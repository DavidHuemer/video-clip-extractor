using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class VideoExtractionViewModel : BaseExtractionViewModel, IExtractionViewModel
{
    public VideoExtractionViewModel(VideoPosition begin, VideoPosition end)
    {
        Begin = new VideoExtractionPartViewModel(begin);
        End = new VideoExtractionPartViewModel(end);
    }

    public override void SetupSelection(Action<IExtractionViewModel> selectionCallback)
    {
        base.SetupSelection(selectionCallback);

        Begin.SetupSelection(selectionCallback);
        End.SetupSelection(selectionCallback);
    }

    #region Properties

    public VideoExtractionPartViewModel Begin { get; set; }

    public VideoExtractionPartViewModel End { get; set; }

    #endregion
}