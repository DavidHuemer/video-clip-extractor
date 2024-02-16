using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class ImageExtractionViewModel(VideoPosition position) : BaseExtractionViewModel
{
    private VideoPosition _position = position;

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