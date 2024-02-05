using BaseUI.Exceptions.Basics;
using Moq;
using VideoClipExtractor.Core.Services.VideoCaching;
using VideoClipExtractor.Core.Services.VideoProvider;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Mocks;

namespace VideoClipExtractor.Tests.Core.Handler.VideoProviderTests;

public class VideoProviderTests
{
    private DependencyMock _dependencyMock = null!;
    private Mock<IVideoRepository> _repo = null!;
    private Mock<IVideoCacheService> _videoCacheServiceMock = null!;

    [SetUp]
    public void Setup()
    {
        _dependencyMock = new DependencyMock();
        _videoCacheServiceMock = new Mock<IVideoCacheService>();
        _repo = RepositoryMocks.GetVideoRepositoryMock();
        _dependencyMock.AddMockDependency(_videoCacheServiceMock);
    }

    [Test]
    public void CallingNextWithoutSetupThrowsException()
    {
        var provider = new VideoProvider(_dependencyMock.Object);
        Assert.Throws<NotSetupException>(() => provider.Next());
    }

    [Test]
    public void SetupIsCalled()
    {
        var project = ProjectExamples.GetExampleProject();
        var provider = new VideoProvider(_dependencyMock.Object);
        provider.Setup(project, _repo.Object);
        _videoCacheServiceMock.Verify(x => x.Setup(It.IsAny<IVideoRepository>(), It.IsAny<string>()), Times.Once);
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(2, 2)]
    [TestCase(10, 10)]
    [TestCase(20, 10)]
    public void RequestCorrectCachingFilesOnInit(int nrSourceVideos, int nrCachedVideos)
    {
        var project = GetProjectWithSourceVideos(nrSourceVideos);

        var provider = new VideoProvider(_dependencyMock.Object);
        provider.Setup(project, _repo.Object);

        _videoCacheServiceMock
            .Verify(x => x.CacheVideo(It.IsAny<SourceVideo>()), Times.Exactly(nrCachedVideos));
    }

    [Test]
    [TestCase(0, 0)]
    [TestCase(1, 1)]
    [TestCase(5, 1)]
    public void VideoProvidedIsInvokedCorrectTimes(int nrSourceVideos, int nrVideoAdded)
    {
        var project = GetProjectWithSourceVideos(nrSourceVideos);

        var videoProvider = new VideoProvider(_dependencyMock.Object);
        videoProvider.Setup(project, _repo.Object);

        var nrRaised = 0;
        videoProvider.VideoAdded += (_, _) => nrRaised++;

        for (var i = 0; i < nrSourceVideos; i++)
            _videoCacheServiceMock.Raise(x => x.VideoCached += null,
                new VideoCachedEventArgs(new CachedVideo(new SourceVideo(), "")));

        Assert.That(nrRaised, Is.EqualTo(nrVideoAdded));
    }

    [Test]
    public void VideoIsCachedOnNext()
    {
        var project = GetProjectWithSourceVideos(15);
        var videoProvider = new VideoProvider(_dependencyMock.Object);
        videoProvider.Setup(project, _repo.Object);

        //Clear method calls on the _videoCacheServiceMock
        _videoCacheServiceMock.Invocations.Clear();

        videoProvider.Next();
        _videoCacheServiceMock.Verify(x => x.CacheVideo(It.IsAny<SourceVideo>()), Times.Once);
    }

    [Test]
    public void VideoIsNotCachedOnNextIfNoVideosLeft()
    {
        var project = GetProjectWithSourceVideos(1);
        var videoProvider = new VideoProvider(_dependencyMock.Object);
        videoProvider.Setup(project, _repo.Object);

        //Clear method calls on the _videoCacheServiceMock
        _videoCacheServiceMock.Invocations.Clear();

        videoProvider.Next();
        _videoCacheServiceMock.Verify(x => x.CacheVideo(It.IsAny<SourceVideo>()), Times.Never);
    }

    [Test]
    public void NextBuffersNextRequests()
    {
        var project = GetProjectWithSourceVideos(20);

        var videoProvider = new VideoProvider(_dependencyMock.Object);
        videoProvider.Setup(project, _repo.Object);

        for (var i = 0; i < 11; i++)
        {
            Console.WriteLine("Hello");
            _videoCacheServiceMock.Raise(x => x.VideoCached += null,
                new VideoCachedEventArgs(new CachedVideo(new SourceVideo(), "")));
        }

        _videoCacheServiceMock.Invocations.Clear();

        var nrRaised = 0;
        videoProvider.VideoAdded += (_, _) => nrRaised++;

        for (var i = 0; i < 11; i++) videoProvider.Next();

        Assert.That(nrRaised, Is.EqualTo(10));

        _videoCacheServiceMock.Raise(x => x.VideoCached += null,
            new VideoCachedEventArgs(new CachedVideo(new SourceVideo(), "")));

        Assert.That(nrRaised, Is.EqualTo(11));
    }

    private static Project GetProjectWithSourceVideos(int nrSourceVideos)
    {
        var project = ProjectExamples.GetExampleProject();
        project.Videos = [];

        for (var i = 0; i < nrSourceVideos; i++) project.Videos.Add(new SourceVideo());

        return project;
    }
}