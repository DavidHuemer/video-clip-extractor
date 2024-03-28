using Moq;
using VideoClipExtractor.Core.Managers.VideoCacheManager;
using VideoClipExtractor.Core.Services.VideoProvider;
using VideoClipExtractor.Core.Services.VideoProvider.CachedVideosService;
using VideoClipExtractor.Core.Services.VideoProvider.RemainingVideosService;
using VideoClipExtractor.Core.Services.VideoProvider.RequestedVideosService;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Mocks;

namespace VideoClipExtractor.Tests.Core.Handler.VideoProviderTests;

public class VideoProviderTests : BaseDependencyTest
{
    private Mock<ICachedVideosService> _cachedVideosService = null!;
    private VideoProvider _provider = null!;
    private Mock<IRemainingVideosService> _remainingVideosService = null!;
    private Mock<IVideoRepository> _repo = null!;
    private Mock<IRequestedVideosService> _requestedVideosService = null!;
    private Mock<IVideoCacheManager> _videoCacheManager = null!;

    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _videoCacheManager = DependencyMock.CreateMockDependency<IVideoCacheManager>();
        _requestedVideosService = DependencyMock.CreateMockDependency<IRequestedVideosService>();
        _remainingVideosService = DependencyMock.CreateMockDependency<IRemainingVideosService>();
        _cachedVideosService = DependencyMock.CreateMockDependency<ICachedVideosService>();
        _repo = RepositoryMocks.GetVideoRepositoryMock();
        DependencyMock.AddMockDependency(_videoCacheManager);
        _provider = new VideoProvider(DependencyMock.Object);
    }

    [Test]
    public void SetupSetsUpServices()
    {
        var project = ProjectExamples.GetExampleProject();
        _provider.Setup(project, _repo.Object);
        _requestedVideosService.Verify(x => x.Setup(project), Times.Once);
        _videoCacheManager.Verify(x => x.Setup(project, _repo.Object), Times.Once);
        _remainingVideosService.Verify(x => x.Setup(project), Times.Once);
    }

    [Test]
    [TestCase(0)]
    [TestCase(2)]
    [TestCase(20)]
    public void WorkingVideosAreCached(int nrWorkingVideos)
    {
        var project = ProjectExamples.GetExampleProject();
        var videos = VideoExamples.GetVideoViewModelExamples(nrWorkingVideos);
        project.WorkingVideos = videos;

        var sourceVideos = videos.Select(video => video.SourceVideo).ToList();

        _provider.Setup(project, _repo.Object);
        _videoCacheManager.Verify(x => x.CacheVideos(sourceVideos), Times.Once);
    }

    [Test]
    [TestCase(0)]
    [TestCase(2)]
    [TestCase(10)]
    public void CacheIsFilled(int maxRemainingVideos)
    {
        var project = ProjectExamples.GetExampleProject();
        _remainingVideosService.SetupGet(x => x.AllowedCacheSize).Returns(maxRemainingVideos);
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        _remainingVideosService.Setup(x => x.GetNextVideo()).Returns(sourceVideo);
        _remainingVideosService.SetupGet(x => x.IsVideoRemaining).Returns(true);

        _provider.Setup(project, _repo.Object);

        _videoCacheManager.Verify(x => x.CacheVideo(It.IsAny<SourceVideo>()), Times.Exactly(maxRemainingVideos));
    }

    [Test]
    public void VideoIsSavedToCached()
    {
        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        _videoCacheManager.Raise(x => x.VideoCached += null, cachedVideo);
        _cachedVideosService.Verify(x => x.Add(cachedVideo), Times.Once);
    }

    [Test]
    public void VideoAddedIsRaisedWhenRequested()
    {
        _requestedVideosService.SetupGet(x => x.IsVideoRequested).Returns(true);
        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        _requestedVideosService.Setup(x => x.GetNextRequestedVideo(It.IsAny<CachedVideo>()))
            .Returns(videoViewModel);

        VideoViewModel? receivedVideo = null;
        _provider.VideoAdded += (video) => receivedVideo = video;
        _videoCacheManager.Raise(x => x.VideoCached += null, cachedVideo);

        Assert.That(receivedVideo, Is.EqualTo(videoViewModel));
    }

    [Test]
    public void NextVideoIsCachedWhenVideoCachedAndVideoRequested()
    {
        _requestedVideosService.SetupGet(x => x.IsVideoRequested).Returns(true);

        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        _remainingVideosService.Setup(x => x.GetNextVideo()).Returns(sourceVideo);
        _remainingVideosService.SetupGet(x => x.IsVideoRemaining).Returns(true);

        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        _videoCacheManager.Raise(x => x.VideoCached += null, cachedVideo);
        _videoCacheManager.Verify(x => x.CacheVideo(sourceVideo), Times.Once);
    }


    [Test]
    public void CacheErrorCallsRequestedVideosService()
    {
        _videoCacheManager.Raise(x => x.Error += null, EventArgs.Empty);
        _requestedVideosService.Verify(x => x.ErrorOccured(), Times.Once);
    }

    [Test]
    public void CacheErrorCachesNextVideo()
    {
        _remainingVideosService.SetupGet(x => x.IsVideoRemaining).Returns(true);
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        _remainingVideosService.Setup(x => x.GetNextVideo()).Returns(sourceVideo);
        _videoCacheManager.Raise(x => x.Error += null, EventArgs.Empty);
        _videoCacheManager.Verify(x => x.CacheVideo(sourceVideo), Times.Once);
    }

    [Test]
    public void NextExtendsCache()
    {
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        _remainingVideosService.Setup(x => x.GetNextVideo()).Returns(sourceVideo);
        _remainingVideosService.SetupGet(x => x.IsVideoRemaining).Returns(true);
        _provider.Next();
        _videoCacheManager.Verify(x => x.CacheVideo(sourceVideo), Times.Once);
    }

    [Test]
    public void NextProvidesNextCachedVideo()
    {
        _cachedVideosService.SetupGet(x => x.IsVideoCached).Returns(true);
        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        _cachedVideosService.Setup(x => x.GetNextCachedVideo()).Returns(cachedVideo);

        var videoViewModel = VideoExamples.GetVideoViewModelExample();
        _requestedVideosService.Setup(x => x.GetNextRequestedVideo(cachedVideo)).Returns(videoViewModel);

        VideoViewModel? receivedVideo = null;
        _provider.VideoAdded += (video) => receivedVideo = video;
        _provider.Next();
        Assert.That(receivedVideo, Is.EqualTo(videoViewModel));
    }

    [Test]
    public void NextRequestsVideoIfNoVideoCached()
    {
        _cachedVideosService.SetupGet(x => x.IsVideoCached).Returns(false);
        _provider.Next();
        _requestedVideosService.Verify(x => x.Request(), Times.Once);
    }
}