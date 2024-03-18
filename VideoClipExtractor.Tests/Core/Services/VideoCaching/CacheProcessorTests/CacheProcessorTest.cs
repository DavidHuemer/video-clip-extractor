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
    [SetUp]
    public void Setup()
    {
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

        // Assert that the cache runner's StoreVideo method was called for each source video
        sourceVideos.ForEach(sourceVideo => _cacheRunner.Verify(x => x.StoreVideo(sourceVideo), Times.Once));
    }
}