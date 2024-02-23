namespace VideoClipExtractor.Data.Videos;

public class Video(CachedVideo cachedVideo)
{
    public string Path { get; } = cachedVideo.LocalPath;

    public string FullName => cachedVideo.SourceVideo.FullName;

    public string Name => cachedVideo.SourceVideo.Name;


    public VideoStatus VideoStatus { get; set; } = VideoStatus.Unset;
}