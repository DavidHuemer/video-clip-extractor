using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class VideoExtraction : BaseExtractionViewModel, IExtraction
{
    public VideoExtraction(VideoPosition begin, VideoPosition end)
    {
        Begin = new VideoExtractionPart(begin);
        End = new VideoExtractionPart(end);
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

    private string _name = "";

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }

    public VideoExtractionPart Begin { get; set; }

    public VideoExtractionPart End { get; set; }

    public int FrameCount => End.Position.Frame - Begin.Position.Frame;

    #endregion
}