using Material.Icons;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.UI.Converters.ExtractionConverters;

namespace VideoClipExtractor.Tests.UI.Converters.ExtractionConverters;

[TestFixture]
[TestOf(typeof(ExtractionTypeToIconConverter))]
public class ExtractionTypeToIconConverterTest
{
    [SetUp]
    public void SetUp()
    {
        _extractionTypeToIconConverter = new ExtractionTypeToIconConverter();
    }

    private ExtractionTypeToIconConverter _extractionTypeToIconConverter = null!;

    [Test]
    public void ImageExtractionReturnsImage()
    {
        var imageExtraction = ExtractionExamples.GetImageExtractionExample();
        var result = _extractionTypeToIconConverter.Convert(imageExtraction, null, null, null);
        Assert.That(result, Is.EqualTo(MaterialIconKind.Image));
    }

    [Test]
    public void VideoExtractionReturnsVideo()
    {
        var videoExtraction = ExtractionExamples.GetVideoExtractionExample();
        var result = _extractionTypeToIconConverter.Convert(videoExtraction, null, null, null);
        Assert.That(result, Is.EqualTo(MaterialIconKind.Video));
    }

    [Test]
    public void NullValueReturnsQuestionMark()
    {
        var result = _extractionTypeToIconConverter.Convert(null, null, null, null);
        Assert.That(result, Is.EqualTo(MaterialIconKind.QuestionMark));
    }


    [Test]
    public void METHOD()
    {
    }
}