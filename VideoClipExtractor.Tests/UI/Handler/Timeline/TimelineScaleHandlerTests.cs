using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline;

public class TimelineScaleHandlerTests
{
    [Test]
    [TestCase(1, 1)]
    [TestCase(2, 1)]
    [TestCase(9, 1)]
    [TestCase(10, 2)]
    [TestCase(27, 5)]
    [TestCase(50, 50)]
    public void GetPrimitiveScaleReturnsCorrectValue(int zoomLevel, int expected)
    {
        // Act
        var result = TimelineScaleHandler.GetPrimitiveScale(zoomLevel);
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    public void NegativeZoomLevelReturnsOne()
    {
        // Act
        var result = TimelineScaleHandler.GetPrimitiveScale(-1);

        Assert.That(result, Is.EqualTo(1));
    }
}