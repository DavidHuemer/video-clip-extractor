using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Tests.Core.Services.VideoServices.VideoPositionServiceTests;

[TestFixture]
[TestOf(typeof(VideoPositionService))]
public class VideoPositionServiceTest
{
    [SetUp]
    public void Setup()
    {
        _videoPositionService = new VideoPositionService();
    }

    private VideoPositionService _videoPositionService = null!;

    [Test]
    public void RequestPositionChangeInvokesEvent()
    {
        var position = new VideoPosition(TimeSpan.Zero, 30);

        _videoPositionService.PositionChangeRequested += (p) => Assert.That(p, Is.EqualTo(position));
        _videoPositionService.RequestPositionChange(position);
    }
}