using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Data.Events;

public class VideoPositionEventArgs(VideoPosition position) : EventArgs
{
    public VideoPosition VideoPosition { get; } = position;
}