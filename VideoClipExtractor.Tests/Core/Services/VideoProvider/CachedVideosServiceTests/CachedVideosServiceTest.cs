using VideoClipExtractor.Core.Exceptions;
using VideoClipExtractor.Core.Services.VideoProvider.CachedVideosService;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.VideoProvider.CachedVideosServiceTests;

[TestFixture]
[TestOf(typeof(CachedVideosService))]
public class CachedVideosServiceTest
{
    [SetUp]
    public void Setup()
    {
        _cachedVideosService = new CachedVideosService();
    }

    private CachedVideosService _cachedVideosService = null!;

    [Test]
    public void IsVideoCachedIsFalseAtBeginning()
    {
        Assert.That(_cachedVideosService.IsVideoCached, Is.False);
    }

    [Test]
    public void AddAddsVideoToCache()
    {
        var video = CachedVideoExamples.GetCachedVideoExample();
        _cachedVideosService.Add(video);
        Assert.That(_cachedVideosService.IsVideoCached, Is.True);
    }

    [Test]
    public void GetNextCachedVideoReturnsVideo()
    {
        var video = CachedVideoExamples.GetCachedVideoExample();
        _cachedVideosService.Add(video);
        var result = _cachedVideosService.GetNextCachedVideo();
        Assert.That(result, Is.EqualTo(video));
    }

    [Test]
    public void GetNextCachedVideoRemovesVideoFromCache()
    {
        var video = CachedVideoExamples.GetCachedVideoExample();
        _cachedVideosService.Add(video);
        _cachedVideosService.GetNextCachedVideo();
        Assert.That(_cachedVideosService.IsVideoCached, Is.False);
    }

    [Test]
    public void GetNextCachedVideoThrowsExceptionWhenCacheIsEmpty()
    {
        Assert.Throws<CachedVideosEmptyException>(() => _cachedVideosService.GetNextCachedVideo());
    }
}