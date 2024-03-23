using BaseUI.Exceptions.Basics;
using Moq;
using VideoClipExtractor.Core.Services.VideoCaching.CacheProcessor;
using VideoClipExtractor.Core.Services.VideoCaching.CacheRunner;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.VideoCaching.CacheProcessorTests;

[TestFixture]
[TestOf(typeof(CacheProcessor))]
public class CacheProcessorTest : BaseDependencyTest
{
    public override void Setup()
    {
        base.Setup();
        _cacheRunner = DependencyMock.CreateMockDependency<ICacheRunner>();
        _videoRepo = new Mock<IVideoRepository>();
        _cacheProcessor = new CacheProcessor(DependencyMock.Object);
    }

    private Mock<ICacheRunner> _cacheRunner = null!;
    private Mock<IVideoRepository> _videoRepo = null!;

    private CacheProcessor _cacheProcessor = null!;

    [Test]
    public void AddVideoWithoutSetupThrowsNotSetupException()
    {
        _cacheRunner.SetupGet(x => x.IsSetup).Returns(false);

        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        Assert.Throws<NotSetupException>(() => _cacheProcessor.AddVideo(sourceVideo));
    }

    [Test]
    public void SetupSetsUpCacheRunner()
    {
        var project = ProjectExamples.GetEmptyProject();
        _cacheProcessor.Setup(project, _videoRepo.Object);

        _cacheRunner.Verify(x => x.Setup(project, _videoRepo.Object), Times.Once);
    }

    [Test]
    public void AddVideoCallsCacheRunner()
    {
        _cacheRunner.SetupGet(x => x.IsSetup).Returns(true);

        var sourceVideos = SourceVideoExamples.GetSourceVideoExamples(5);
        sourceVideos.ForEach(sourceVideo => _cacheProcessor.AddVideo(sourceVideo));
        sourceVideos.ForEach(sourceVideo => _cacheRunner.Verify(x => x.StoreVideo(sourceVideo), Times.Once));
    }

    [Test]
    public void OnResultProcessedIsInvoked()
    {
        _cacheRunner.SetupGet(x => x.IsSetup).Returns(true);
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        var cachedVideo = CachedVideoExamples.GetCachedVideoExample();
        _cacheRunner.Setup(x => x.StoreVideo(sourceVideo)).Returns(cachedVideo);
        _cacheProcessor.OnResultProcessed += video => Assert.That(video, Is.EqualTo(cachedVideo));
        _cacheProcessor.AddVideo(sourceVideo);
    }

    [Test]
    public void ErrorIsInvoked()
    {
        _cacheRunner.SetupGet(x => x.IsSetup).Returns(true);
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        var exception = new Exception();
        _cacheRunner.Setup(x => x.StoreVideo(sourceVideo)).Throws(exception);
        _cacheProcessor.OnErrorOccurred += ex => Assert.That(ex, Is.EqualTo(exception));
        _cacheProcessor.AddVideo(sourceVideo);
    }
}