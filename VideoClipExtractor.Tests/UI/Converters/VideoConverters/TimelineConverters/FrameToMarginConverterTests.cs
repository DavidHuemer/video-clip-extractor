using System.Globalization;
using System.Windows;
using Moq;
using VideoClipExtractor.UI.Converters.VideoConverters.TimelineConverters;
using VideoClipExtractor.UI.Handler.Timeline;

namespace VideoClipExtractor.Tests.UI.Converters.VideoConverters.TimelineConverters;

public class FrameToMarginConverterTests
{
    private FrameToMarginConverter _frameToMarginConverter = null!;
    private Mock<ITimelineFrameWidthHandler> _timelineFrameWidthHandler = null!;

    [SetUp]
    public void Setup()
    {
        _timelineFrameWidthHandler = new Mock<ITimelineFrameWidthHandler>();
        _frameToMarginConverter = new FrameToMarginConverter(_timelineFrameWidthHandler.Object);
    }

    [Test]
    [TestCase(0, 100, 0)]
    [TestCase(1, 100, 100)]
    [TestCase(2, 100, 200)]
    [TestCase(2, 50, 100)]
    public void ConvertFrameToMarginReturnsCorrectValue(int frame, double frameWidth, double expected)
    {
        _timelineFrameWidthHandler.Setup(x => x.GetFrameWidth(It.IsAny<int>())).Returns(frameWidth);

        var result = _frameToMarginConverter.Convert(new object?[] { frame, 0 }, typeof(Thickness), null,
            CultureInfo.CurrentCulture);

        Assert.That(result, Is.EqualTo(new Thickness(expected, 0, 0, 0)));
    }

    [Test]
    public void ConvertFrameWithNoFrameReturnsDefaultMargin()
    {
        var result = _frameToMarginConverter.Convert(new object?[] { null, 1 }, typeof(Thickness), null,
            CultureInfo.CurrentCulture);

        Assert.That(result, Is.EqualTo(new Thickness(0, 0, 0, 0)));
    }

    [Test]
    public void ConvertFrameWithNoZoomLevelReturnsDefaultMargin()
    {
        var converter = new FrameToMarginConverter();

        var result = converter.Convert(new object?[] { 1, null }, typeof(Thickness), null, CultureInfo.CurrentCulture);

        Assert.That(result, Is.EqualTo(new Thickness(0, 0, 0, 0)));
    }
}