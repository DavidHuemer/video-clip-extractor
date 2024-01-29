namespace VideoClipExtractor.Data.Videos.Events;

public class SourceVideoEventArgs(SourceVideo sourceVideo) : EventArgs
{
    public SourceVideo SourceVideo { get; set; } = sourceVideo;
}