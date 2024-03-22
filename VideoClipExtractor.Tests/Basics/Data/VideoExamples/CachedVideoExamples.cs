using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Basics.Data.VideoExamples;

/// <summary>
/// Contains examples of cached videos
/// </summary>
public static class CachedVideoExamples
{
    private const string CachedVideoName = "Video";
    private const string LocalPath = @$"C:\Cached\{CachedVideoName}.mp4";


    public static CachedVideo GetCachedVideoExample(string name = CachedVideoName)
    {
        var sourcePath = @$"{VideoRepositoryExamples.VideoRepositoryPath}\{name}.mp4";
        var localPath = @$"C:\Cached\{name}.mp4";

        return new CachedVideo(SourceVideoExamples.GetSourceVideoExample(sourcePath), localPath);
    }

    public static CachedVideo GetCachedVideoExampleBySourceVideo(SourceVideo sourceVideo) =>
        new CachedVideo(sourceVideo, GetLocalPath(sourceVideo.FullName));

    public static string GetLocalPath(string fullName) => @$"C:\Cached\{fullName}";
}