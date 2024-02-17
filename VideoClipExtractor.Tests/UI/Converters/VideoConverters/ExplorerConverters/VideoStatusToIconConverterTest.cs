using System.Globalization;
using Material.Icons;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.Converters.VideoConverters.ExplorerConverters;

namespace VideoClipExtractor.Tests.UI.Converters.VideoConverters.ExplorerConverters;

[TestFixture]
[TestOf(typeof(VideoStatusToIconConverter))]
public class VideoStatusToIconConverterTest
{
    [SetUp]
    public void Setup()
    {
        _videoStatusToIconConverter = new VideoStatusToIconConverter();
    }

    private VideoStatusToIconConverter _videoStatusToIconConverter = null!;


    [Test]
    [TestCase(VideoStatus.Unset, MaterialIconKind.QuestionMark)]
    [TestCase(VideoStatus.Skipped, MaterialIconKind.Pin)]
    [TestCase(VideoStatus.ReadyForExport, MaterialIconKind.Check)]
    [TestCase(VideoStatus.Exported, MaterialIconKind.AlertCircle)]
    [TestCase(null, MaterialIconKind.AlertCircle)]
    public void ConvertReturnsCorrectIcon(VideoStatus? status, MaterialIconKind expected)
    {
        var result =
            _videoStatusToIconConverter.Convert(status, typeof(MaterialIconKind), null, CultureInfo.InvariantCulture);
        Assert.That(result, Is.EqualTo(expected));
    }
}