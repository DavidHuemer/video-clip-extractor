using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline;

public class TimelineFrameWidthHandlerTests
{
    [Test]
    [TestCase(0, 100)]
    [TestCase(9, 50)]
    public void GetFrameWidthReturnsCorrectValue(int zoomLevel, double expected)
    {
        // Act
        var result = TimelineFrameWidthHandler.GetFrameWidth(zoomLevel);
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}