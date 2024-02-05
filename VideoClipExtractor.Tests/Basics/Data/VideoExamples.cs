using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Basics.Data;

public static class VideoExamples
{
    public static Video GetVideoExample()
    {
        return new Video(new CachedVideo(new SourceVideo(), ""));
    }
}