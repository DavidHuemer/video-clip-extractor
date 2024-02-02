namespace VideoClipExtractor.Data.Videos.Events;

public class VideoEventArgs(Video video) : EventArgs
{
    public Video Video { get; } = video;
}