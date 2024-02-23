using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Basics.Data;

/// <summary>
/// Contains examples of video data.
/// </summary>
public static class VideoExamples
{
    private const string SourcePath = @"C:\Source\Video.mp4";
    private const string LocalPath = @"C:\Cached\Video.mp4";

    public static Video GetVideoExample()
    {
        return new Video(new CachedVideo(GetSourceVideo(), ""));
    }

    public static VideoViewModel GetVideoViewModelExample()
    {
        return new VideoViewModel(GetVideoExample());
    }

    public static CachedVideo GetCachedVideoExample()
    {
        return new CachedVideo(GetSourceVideo(), LocalPath);
    }

    /// <summary>
    /// Returns a source video example.
    /// </summary>
    /// <returns></returns>
    public static SourceVideo GetSourceVideo()
    {
        return new SourceVideo(SourcePath, 1048);
    }
}