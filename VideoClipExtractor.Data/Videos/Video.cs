namespace VideoClipExtractor.Data.Videos;

public class Video
{
    public Video(CachedVideo cachedVideo)
    {
        Path = cachedVideo.LocalPath;
    }

    public string Path { get; set; }

    public VideoStatus VideoStatus { get; set; } = VideoStatus.Unset;
}