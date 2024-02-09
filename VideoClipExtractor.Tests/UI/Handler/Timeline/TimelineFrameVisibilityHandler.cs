using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline;

public class TimelineFrameVisibilityHandlerTests
{
    [Test]
    [TestCase(0, 100, 0)]
    [TestCase(50, 100, 0)]
    [TestCase(100, 100, 0)]
    [TestCase(200, 100, 0)]
    [TestCase(201, 100, 0)]
    [TestCase(300, 100, 1)]
    [TestCase(350, 100, 2)]
    [TestCase(399, 100, 2)]
    [TestCase(400, 100, 2)]
    [TestCase(200, 50, 0)]
    [TestCase(250, 50, 1)]
    public void GetFirstVisibleFrameReturnsCorrectValue(double movementPosition, double frameWidth, int expected)
    {
        // Act
        var result = TimelineFrameVisibilityHandler
            .GetFirstVisibleFrame(movementPosition, frameWidth);

        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}