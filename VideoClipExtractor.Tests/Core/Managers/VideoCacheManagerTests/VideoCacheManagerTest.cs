using BaseUI.Exceptions.Basics;
using Moq;
using VideoClipExtractor.Core.Managers.VideoCacheManager;
using VideoClipExtractor.Core.Services.VideoCaching;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Managers.VideoCacheManagerTests;

[TestFixture]
[TestOf(typeof(VideoCacheManager))]
public class VideoCacheManagerTest : BaseDependencyTest
{
    private Mock<IVideoCacheService> _cacheService = null!;
    private VideoCacheManager _videoCacheManager = null!;

    public override void Setup()
    {
        base.Setup();
        _cacheService = DependencyMock.CreateMockDependency<IVideoCacheService>();
        _videoCacheManager = new VideoCacheManager(DependencyMock.Object);
    }

    [Test]
    public void CacheVideoWithoutSetupThrowsNotSetupException()
    {
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        Assert.Throws<NotSetupException>(() => _videoCacheManager.CacheVideo(sourceVideo));
    }

    [Test]
    public void CacheVideosWithoutSetupThrowsNotSetupException()
    {
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(4);
        Assert.Throws<NotSetupException>(() => _videoCacheManager.CacheVideos(sourceVideos));
    }

    [Test]
    public void SetupSetsUpCacheService()
    {
        var project = ProjectExamples.GetExampleProject();
        var repo = new Mock<IVideoRepository>();

        _videoCacheManager.Setup(project, repo.Object);
        _cacheService.Verify(x => x.Setup(project, repo.Object), Times.Once);
    }

    [Test]
    public void CacheVideoCallsCacheService()
    {
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        _videoCacheManager.Setup(ProjectExamples.GetExampleProject(), new Mock<IVideoRepository>().Object);
        _videoCacheManager.CacheVideo(sourceVideo);
        _cacheService.Verify(x => x.CacheVideo(sourceVideo), Times.Once);
    }

    [Test]
    public void CacheVideosCallsCacheService()
    {
        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(4);
        _videoCacheManager.Setup(ProjectExamples.GetExampleProject(), new Mock<IVideoRepository>().Object);
        _videoCacheManager.CacheVideos(sourceVideos);

        sourceVideos.ForEach(video => _cacheService.Verify(x => x.CacheVideo(video), Times.Once));
    }

    [Test]
    public void CacheServiceVideoCachedEventInvokesManager()
    {
        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        _videoCacheManager.Setup(ProjectExamples.GetExampleProject(), new Mock<IVideoRepository>().Object);

        CachedVideo? resultingCachedVideo = null;
        _videoCacheManager.VideoCached += (video) => resultingCachedVideo = video;
        _cacheService.Raise(x => x.VideoCached += null!, cachedVideo);
        Assert.That(resultingCachedVideo, Is.EqualTo(cachedVideo));
    }

    [Test]
    public void CacheServiceErrorInvokesManager()
    {
        _videoCacheManager.Setup(ProjectExamples.GetExampleProject(), new Mock<IVideoRepository>().Object);
        var errorInvoked = false;
        _videoCacheManager.Error += (_, _) => errorInvoked = true;
        _cacheService.Raise(x => x.Error += null!, new Exception());
        Assert.True(errorInvoked);
    }

    [Test]
    public void AfterSecondSetupFirstCacheServiceShouldNotInvoke()
    {
        _videoCacheManager.Setup(ProjectExamples.GetExampleProject(), new Mock<IVideoRepository>().Object);
        DependencyMock.Setup(x => x.GetDependency<IVideoCacheService>()).Returns(new Mock<IVideoCacheService>().Object);
        _videoCacheManager.Setup(ProjectExamples.GetExampleProject(), new Mock<IVideoRepository>().Object);

        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        _videoCacheManager.VideoCached += (_) => Assert.Fail("Should not be invoked");
        _videoCacheManager.Error += (_, _) => Assert.Fail("Should not be invoked");
        _cacheService.Raise(x => x.VideoCached += null!, cachedVideo);
        _cacheService.Raise(x => x.Error += null!, this, EventArgs.Empty);
    }
}