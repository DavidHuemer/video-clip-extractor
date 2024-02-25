using BaseUI.Exceptions.Basics;
using VideoClipExtractor.Core.Services.VideoCaching;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Services.VideoCaching;

public class VideoCacheServiceTests
{
    private VideoCacheService? _videoCacheService;

    [SetUp]
    public void Setup()
    {
        _videoCacheService = new VideoCacheService();
    }

    [Test]
    public void ThrowsNotSetupExceptionWhenCacheVideoIsCalledBeforeSetup()
    {
        Assert.Throws<NotSetupException>(() => _videoCacheService?.CacheVideo(VideoExamples.GetSourceVideo()));
    }
}