namespace VideoClipExtractor.Data.Videos;

public class RequestedVideo
{
    private readonly VideoViewModel? _video;

    public RequestedVideo(VideoViewModel video)
    {
        _video = video;
    }

    public RequestedVideo()
    {
    }

    public bool HasVideo => _video != null;

    public VideoViewModel? Video => _video;
}