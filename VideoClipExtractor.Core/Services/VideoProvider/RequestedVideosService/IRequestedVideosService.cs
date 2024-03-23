using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoProvider.RequestedVideosService;

public interface IRequestedVideosService
{
    bool IsVideoRequested { get; }
    void Setup(Project project);

    void RequestVideos(List<VideoViewModel> videos);

    void Request();

    void ErrorOccured();

    VideoViewModel GetNextRequestedVideo(CachedVideo video);
}