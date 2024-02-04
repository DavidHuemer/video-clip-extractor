namespace VideoClipExtractor.Data.Videos.Events;

public class VideoChangedEventArgs(Video? video) : EventArgs
{
    public Video? Video { get; } = video;
}