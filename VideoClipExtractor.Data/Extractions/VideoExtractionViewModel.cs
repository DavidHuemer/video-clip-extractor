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

    //public override VideoPosition Position => Begin.Position;

    public override VideoPosition Position
    {
        get => Begin.Position;
        set
        {
            var oldPosition = Begin.Position;

            Begin.Position = value;
            End.Position = new VideoPosition(End.Position.Frame - oldPosition.Frame + value.Frame);
        }
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

    public int FrameCount => End.Position.Frame - Begin.Position.Frame;

    #endregion
}