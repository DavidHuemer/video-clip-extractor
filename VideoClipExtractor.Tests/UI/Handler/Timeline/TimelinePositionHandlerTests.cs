using Moq;
using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.Tests.UI.Handler.Timeline;

public class TimelinePositionHandlerTests
{
    private Mock<ITimelineFrameWidthHandler> _timelineFrameWidthHandlerMock = null!;

    private TimelinePositionHandler _timelinePositionHandler = null!;

    [SetUp]
    public void Setup()
    {
        _timelineFrameWidthHandlerMock = new Mock<ITimelineFrameWidthHandler>();
        _timelinePositionHandler = new TimelinePositionHandler(_timelineFrameWidthHandlerMock.Object);
    }

    [Test]
    [TestCase(0, 100, 0)]
    [TestCase(200, 100, 0)]
    [TestCase(300, 100, 1)]
    [TestCase(250, 100, 0.5)]
    [TestCase(200, 50, 0)]
    [TestCase(250, 50, 1)]
    public void GetFrameAtPositionReturnsCorrectValue(double position, double frameWidth, double expected)
    {
        // Arrange
        _timelineFrameWidthHandlerMock.Setup(x => x.GetFrameWidth(It.IsAny<int>())).Returns(frameWidth);
        // Act
        var result = _timelinePositionHandler.GetFrameAtPosition(position, 1);
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(0, 100, 200)]
    [TestCase(1, 100, 300)]
    [TestCase(1.5, 100, 350)]
    public void GetPositionAtFrameReturnsCorrectValue(double framePosition, int frameWidth, double expected)
    {
        // Arrange
        _timelineFrameWidthHandlerMock.Setup(x => x.GetFrameWidth(It.IsAny<int>())).Returns(frameWidth);
        // Act
        var result = _timelinePositionHandler.GetPositionAtFrame(framePosition, 1);
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }

    [Test]
    [TestCase(0, 1000, 500)]
    [TestCase(100, 1000, 600)]
    public void GetCenterPositionReturnsCorrectValue(double timelinePosition, double timelineWidth, double expected)
    {
        // Act
        var result = _timelinePositionHandler.GetCenterPosition(timelinePosition, timelineWidth);
        // Assert
        Assert.That(result, Is.EqualTo(expected));
    }
}