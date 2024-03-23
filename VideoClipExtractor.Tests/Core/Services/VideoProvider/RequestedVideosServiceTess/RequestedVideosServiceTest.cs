using VideoClipExtractor.Core.Exceptions;
using VideoClipExtractor.Core.Services.VideoProvider.RequestedVideosService;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.VideoProvider.RequestedVideosServiceTess;

[TestFixture]
[TestOf(typeof(RequestedVideosService))]
public class RequestedVideosServiceTest
{
    [SetUp]
    public void Setup()
    {
        _requestedVideosService = new RequestedVideosService();
    }

    private RequestedVideosService _requestedVideosService = null!;

    [Test]
    public void IsVideoRequestedIsFalseAtBeginning()
    {
        Assert.That(_requestedVideosService.IsVideoRequested, Is.False);
    }

    [Test]
    public void SetupWithWorkingVideosRequestsWorkingVideos()
    {
        var project = ProjectExamples.GetEmptyProject();
        var workingVideos = VideoExamples.GetVideoViewModelExamples(4);
        project.WorkingVideos = workingVideos;

        _requestedVideosService.Setup(project);
        Assert.That(_requestedVideosService.IsVideoRequested, Is.True);
    }

    [Test]
    public void SetupWithoutWorkingVideosRequestsVideo()
    {
        var project = ProjectExamples.GetEmptyProject();
        _requestedVideosService.Setup(project);
        Assert.That(_requestedVideosService.IsVideoRequested, Is.True);
    }

    [Test]
    public void GetNextRequestedVideoWithNoRequestsThrowsException()
    {
        Assert.Throws<RequestedVideosEmptyException>(() =>
            _requestedVideosService.GetNextRequestedVideo(CachedVideoExamples.GetCachedVideoExample()));
    }

    [Test]
    public void GetNextRequestedVideoWithNormalRequestReturnsVideoViewModel()
    {
        var project = ProjectExamples.GetEmptyProject();
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(4);
        project.Videos = sourceVideos;

        _requestedVideosService.Setup(project);
        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        var requestedVideo = _requestedVideosService.GetNextRequestedVideo(cachedVideo);

        var expected = new VideoViewModel(cachedVideo);
        Assert.That(requestedVideo, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextRequestedVideoWithWorkingVideosReturnsVideoViewModel()
    {
        var project = ProjectExamples.GetEmptyProject();
        var workingVideos = new List<VideoViewModel>
        {
            VideoExamples.GetVideoViewModelExample(),
        };
        project.WorkingVideos = workingVideos;

        _requestedVideosService.Setup(project);
        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        var requestedVideo = _requestedVideosService.GetNextRequestedVideo(cachedVideo);

        var expected = VideoExamples.GetVideoViewModelExample();
        expected.LocalPath = cachedVideo.LocalPath;
        Assert.That(requestedVideo, Is.EqualTo(expected));
    }

    [Test]
    public void GetNextRequestedVideoDecreasesRequestedVideosCount()
    {
        var project = ProjectExamples.GetEmptyProject();
        var workingVideos = new List<VideoViewModel>
        {
            VideoExamples.GetVideoViewModelExample(),
        };
        project.WorkingVideos = workingVideos;

        _requestedVideosService.Setup(project);
        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        _requestedVideosService.GetNextRequestedVideo(cachedVideo);

        Assert.That(_requestedVideosService.IsVideoRequested, Is.False);
    }

    [Test]
    public void ErrorDecreasesVideosCount()
    {
        var project = ProjectExamples.GetEmptyProject();
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(4);
        project.Videos = sourceVideos;

        _requestedVideosService.Setup(project);
        _requestedVideosService.ErrorOccured();
        Assert.That(_requestedVideosService.IsVideoRequested, Is.False);
    }
}