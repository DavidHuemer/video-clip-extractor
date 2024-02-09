using System.Globalization;
using System.Windows;
using VideoClipExtractor.UI.Converters.VideoConverters.TimelineConverters;

namespace VideoClipExtractor.Tests.UI.Converters.VideoConverters.TimelineConverters;

public class FrameToMarginConverterTests
{
    [Test]
    [TestCase(0, 1, 0)]
    public void ConvertFrameToMarginReturnsCorrectValue(int frame, int zoomLevel, double expected)
    {
        var converter = new FrameToMarginConverter();

        var result = converter.Convert(new object[] { frame, zoomLevel }, typeof(Thickness), null,
            CultureInfo.CurrentCulture);

        Assert.That(result, Is.EqualTo(new Thickness(expected, 0, 0, 0)));
    }

    [Test]
    public void ConvertFrameWithNoFrameReturnsDefaultMargin()
    {
        var converter = new FrameToMarginConverter();

        var result = converter.Convert(new object[] { null, 1 }, typeof(Thickness), null, CultureInfo.CurrentCulture);

        Assert.That(result, Is.EqualTo(new Thickness(0, 0, 0, 0)));
    }

    [Test]
    public void ConvertFrameWithNoZoomLevelReturnsDefaultMargin()
    {
        var converter = new FrameToMarginConverter();

        var result = converter.Convert(new object[] { 1, null }, typeof(Thickness), null, CultureInfo.CurrentCulture);

        Assert.That(result, Is.EqualTo(new Thickness(0, 0, 0, 0)));
    }
}