using System.Globalization;
using Material.Icons;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Converters.VideoConverters;

namespace VideoClipExtractor.Tests.UI.Converters.VideoConverters;

public class PlayStatusToIconConverterTests
{
    private PlayStatusToIconKindConverter _converter = null!;

    [SetUp]
    public void Setup()
    {
        _converter = new PlayStatusToIconKindConverter();
    }

    [Test]
    [TestCase(PlayStatus.Playing, MaterialIconKind.Pause)]
    [TestCase(PlayStatus.Paused, MaterialIconKind.Play)]
    public void ConverterReturnsCorrectIcon(PlayStatus playStatus, MaterialIconKind icon)
    {
        // Act
        var result = _converter.Convert(playStatus, typeof(MaterialIconKind), null, CultureInfo.CurrentCulture);

        // Assert
        Assert.That(result, Is.EqualTo(icon));
    }
}