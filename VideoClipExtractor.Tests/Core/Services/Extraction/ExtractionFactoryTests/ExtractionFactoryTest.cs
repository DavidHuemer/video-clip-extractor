using VideoClipExtractor.Core.Services.Extraction.ExtractionFactory;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.Extraction.ExtractionFactoryTests;

[TestFixture]
[TestOf(typeof(ExtractionFactory))]
public class ExtractionFactoryTest
{
    [SetUp]
    public void Setup()
    {
        _extractionFactory = new ExtractionFactory();
    }

    private ExtractionFactory _extractionFactory = null!;

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(250)]
    public void GetImageExtractionReturnsCorrectImageExtraction(int frame)
    {
        var position = new VideoPosition(frame);
        var imageExtraction = _extractionFactory.GetImageExtraction(position);
        Assert.That(imageExtraction.Position.Frame, Is.EqualTo(frame));
    }

    [Test]
    public void GetVideoExtractionReturnsCorrectVideoExtraction()
    {
        var video = VideoExamples.GetVideoViewModelExample();

        var position = new VideoPosition(0);
        var videoExtraction = _extractionFactory.GetVideoExtraction(position, video);
        Assert.That(videoExtraction.Begin.Position.Frame, Is.EqualTo(0));
    }
}