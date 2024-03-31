namespace VideoClipExtractor.Data.Videos;

public class VideoProviderQueue
{
    public VideoProviderQueue(Project.Project project)
    {
        var sourceVideosList = project.Videos.ToList()
            .Where(video => !video.Checked)
            .OrderByDescending(sourceVideo => sourceVideo.Size)
            .ThenBy(sourceVideo => sourceVideo.FullName)
            .ToList();

        RemainingVideos = new Queue<SourceVideo>(sourceVideosList);
    }

    public Queue<SourceVideo> RemainingVideos { get; set; }
}