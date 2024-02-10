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
    [TestCase(0, 100)]
    [TestCase(9, 50)]
    public void GetFrameWidthReturnsCorrectValue(int zoomLevel, double expected)
    {
        // Act
        var result = _timelineFrameWidthHandler.GetFrameWidth(zoomLevel);
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}