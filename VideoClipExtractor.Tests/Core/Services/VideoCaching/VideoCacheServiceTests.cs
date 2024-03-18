using BaseUI.Exceptions.Basics;
using Moq;
using VideoClipExtractor.Core.Services.VideoCaching;
using VideoClipExtractor.Core.Services.VideoCaching.CacheProcessor;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.VideoCaching;

public class VideoCacheServiceTests : BaseDependencyTest
{
    private Mock<ICacheProcessor> _cacheProcessorMock = null!;
    private VideoCacheService _videoCacheService = null!;

    public override void Setup()
    {
        base.Setup();
        _cacheProcessorMock = DependencyMock.CreateMockDependency<ICacheProcessor>();
        _videoCacheService = new VideoCacheService(DependencyMock.Object);
    }

    [Test]
    public void ThrowsNotSetupExceptionWhenCacheVideoIsCalledBeforeSetup()
    {
        _cacheProcessorMock.SetupGet(x => x.IsSetup).Returns(false);

        Assert.Throws<NotSetupException>(() =>
            _videoCacheService?.CacheVideo(SourceVideoExamples.GetSourceVideoExample()));
    }

    [Test]
    public void SetupSetsUpCacheProcessor()
    {
        var project = ProjectExamples.GetEmptyProject();
        var repository = new Mock<IVideoRepository>();

        _videoCacheService.Setup(project, repository.Object);

        _cacheProcessorMock.Verify(x => x.Setup(project, repository.Object), Times.Once);
    }

    [Test]
    public void CacheVideoCallsCacheProcessor()
    {
        _cacheProcessorMock.SetupGet(x => x.IsSetup).Returns(true);
        var sourceVideo = SourceVideoExamples.GetSourceVideoExample();
        _videoCacheService.CacheVideo(sourceVideo);

        _cacheProcessorMock.Verify(x => x.AddVideo(sourceVideo), Times.Once);
    }
}