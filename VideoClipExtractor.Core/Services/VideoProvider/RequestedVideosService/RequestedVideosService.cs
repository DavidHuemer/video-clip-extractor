using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.Core.Exceptions;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoProvider.RequestedVideosService;

[Transient]
public class RequestedVideosService : IRequestedVideosService
{
    private readonly Queue<RequestedVideo> _requestedVideos = new();

    public void Setup(Project project)
    {
        if (project.WorkingVideos.Count > 0)
        {
            RequestVideos(project.WorkingVideos);
        }
        else
        {
            Request();
        }
    }

    public void RequestVideos(List<VideoViewModel> videos)
    {
        foreach (var video in videos)
            _requestedVideos.Enqueue(new RequestedVideo(video));
    }

    public void Request()
    {
        _requestedVideos.Enqueue(new RequestedVideo());
    }

    public void ErrorOccured()
    {
        if (IsVideoRequested)
            _requestedVideos.Dequeue();
    }

    public VideoViewModel GetNextRequestedVideo(CachedVideo cachedVideo)
    {
        if (!IsVideoRequested)
            throw new RequestedVideosEmptyException();

        var requestedVideo = _requestedVideos.Dequeue();
        if (requestedVideo.Video == null) return new VideoViewModel(cachedVideo);

        var video = requestedVideo.Video;
        video.LocalPath = cachedVideo.LocalPath;
        return video;
    }

    public bool IsVideoRequested => _requestedVideos.Count > 0;
}