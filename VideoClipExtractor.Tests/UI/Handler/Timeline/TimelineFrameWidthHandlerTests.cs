using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline;

public class TimelineFrameWidthHandlerTests
{
    private TimelineFrameWidthHandler _timelineFrameWidthHandler = null!;

    [SetUp]
    public void Setup()
    {
        _timelineFrameWidthHandler = new TimelineFrameWidthHandler();
    }

    [Test]
    public void GetFrameWidthReturnsValue()
    {
        // Act
        var result = _timelineFrameWidthHandler.GetFrameWidth(20);
        // Assert
        Assert.That(result, Is.GreaterThan(0));
    }
}