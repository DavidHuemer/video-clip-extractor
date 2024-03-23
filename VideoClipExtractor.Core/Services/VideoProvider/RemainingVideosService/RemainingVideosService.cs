using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.Core.Exceptions;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoProvider.RemainingVideosService;

/// <summary>
/// Responsible for managing the remaining videos that are not yet processed.
/// </summary>
[Transient]
public class RemainingVideosService : IRemainingVideosService
{
    private Queue<SourceVideo> _remainingVideos = new();

    public int RemainingVideosCount => _remainingVideos.Count;

    public int AllowedCacheSize => Math.Min(VideoProvider.CacheSize, RemainingVideosCount);
    public bool IsVideoRemaining => _remainingVideos.Count > 0;

    public void Setup(Project project)
    {
        var sourceVideos = project.Videos
            .Where(video => !IsVideoAlreadyWorking(video, project.WorkingVideos));

        _remainingVideos = new Queue<SourceVideo>(sourceVideos);
    }

    public SourceVideo GetNextVideo()
    {
        if (!IsVideoRemaining)
            throw new RemainingVideosEmptyException();

        return _remainingVideos.Dequeue();
    }

    private static bool IsVideoAlreadyWorking(SourceVideo sourceVideo, List<VideoViewModel> workingVideos) =>
        workingVideos.Select(x => x.SourceVideo).Any(workingVideo => workingVideo.Equals(sourceVideo));
}