using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Events;

public class VideoPositionEventArgs(VideoPosition1 position1) : EventArgs
{
    public VideoPosition1 VideoPosition1 { get; } = position1;
}