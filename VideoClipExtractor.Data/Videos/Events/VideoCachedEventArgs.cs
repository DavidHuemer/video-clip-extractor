namespace VideoClipExtractor.Data.Videos.Events;

public class VideoCachedEventArgs(CachedVideo cachedVideo) : EventArgs
{
    /// <summary>
    /// The cached video
    /// </summary>
    public CachedVideo CachedVideo { get; set; } = cachedVideo;
}