namespace VideoClipExtractor.Data.Videos;

public class Video
{
    public Video(CachedVideo cachedVideo)
    {
        Path = cachedVideo.LocalPath;
        Name = cachedVideo.SourceVideo.Name;
    }

    public string Path { get; set; }

    public string Name { get; set; }

    public VideoStatus VideoStatus { get; set; } = VideoStatus.Unset;
}