using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.Core.Exceptions;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoProvider.RemainingVideosService;

[Transient]
public class RemainingVideosService : IRemainingVideosService
{
    private Queue<SourceVideo> _remainingVideos = new();

    public void Setup(Project project)
    {
        var sourceVideos = project.Videos
            .Where(video => !IsVideoAlreadyWorking(video, project.WorkingVideos));

        _remainingVideos = new Queue<SourceVideo>(sourceVideos);
    }

    public int RemainingVideosCount => _remainingVideos.Count;

    public SourceVideo GetNextVideo()
    {
        if (_remainingVideos.Count == 0)
            throw new RemainingVideosEmptyException();

        return _remainingVideos.Dequeue();
    }

    private static bool IsVideoAlreadyWorking(SourceVideo sourceVideo, List<VideoViewModel> workingVideos) =>
        workingVideos.Select(x => x.SourceVideo).Any(workingVideo => workingVideo.Equals(sourceVideo));
}