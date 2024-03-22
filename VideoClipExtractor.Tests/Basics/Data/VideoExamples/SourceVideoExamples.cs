using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Basics.Data.VideoExamples;

/// <summary>
/// Contains examples of source videos
/// </summary>
public static class SourceVideoExamples
{
    public const string SourcePath = @$"{VideoRepositoryExamples.VideoRepositoryPath}\Video.mp4";

    public static SourceVideo
        GetSourceVideoExample(string path = SourcePath, int size = 1048, bool isChecked = false) =>
        new(path, size) { Checked = isChecked };

    private static SourceVideo GetSourceVideoExampleByName(string name, int size = 1048, bool isChecked = false)
    {
        var path = @$"{VideoRepositoryExamples.VideoRepositoryPath}\{name}.mp4";
        return GetSourceVideoExample(path, size);
    }

    public static SourceVideo GetSourceVideoExampleByFullName(string fullName, int size = 1048, bool isChecked = false)
    {
        var path = GetSourcePath(fullName);
        return GetSourceVideoExample(path, size, isChecked);
    }

    public static List<SourceVideo> GetSourceVideoExamples(int nrVideos)
    {
        var videos = new List<SourceVideo>();
        for (var i = 0; i < nrVideos; i++)
        {
            var name = $"Video{i}";
            videos.Add(GetSourceVideoExampleByName(name));
        }

        return videos;
    }

    /// <summary>
    /// Returns a list of realistic source videos.
    /// <para>First 4 videos are already checked</para>
    /// </summary>
    /// <returns>Realistic source videos</returns>
    public static List<SourceVideo> GetRealisticSourceVideos()
    {
        return Enumerable.Range(0, RealisticVideos.RealisticSourceVideosCount)
            .Select(videoIndex => GetSourceVideoExampleByName($"Video {videoIndex}",
                isChecked: videoIndex <= RealisticVideos.ExtractedVideosCount))
            .ToList();
    }

    public static string GetSourcePath(string fullName) => @$"{VideoRepositoryExamples.VideoRepositoryPath}\{fullName}";
}