using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class VideoExtractionPart(VideoPosition videoPosition) : BaseExtractionViewModel
{
    private VideoPosition _position = videoPosition;

    public override VideoPosition Position
    {
        get => _position;
        set
        {
            _position = value;
            OnPropertyChanged();
        }
    }
}