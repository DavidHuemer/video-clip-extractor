using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Extractions.Basics;

public interface IExtractionViewModel : ISelectAble
{
    VideoPosition Position { get; set; }
}