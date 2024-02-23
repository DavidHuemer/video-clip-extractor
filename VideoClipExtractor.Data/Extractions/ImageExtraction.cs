using VideoClipExtractor.Data.Extractions.Basics;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions;

public class ImageExtraction(VideoPosition position) : BaseExtractionViewModel, IExtraction
{
    private string _name = "";

    public override VideoPosition Position
    {
        get => position;
        set
        {
            position = value;
            OnPropertyChanged();
        }
    }

    public string Name
    {
        get => _name;
        set
        {
            _name = value;
            OnPropertyChanged();
        }
    }
}