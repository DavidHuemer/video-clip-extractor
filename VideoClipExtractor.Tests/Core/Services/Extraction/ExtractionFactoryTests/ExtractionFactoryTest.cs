using FFMpeg.Wrapper.Data;
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

    private const int FrameRate = 50;

    private ExtractionFactory _extractionFactory = null!;

    [Test]
    [TestCase(0)]
    [TestCase(1)]
    [TestCase(250)]
    public void GetImageExtractionReturnsCorrectImageExtraction(int frame)
    {
        var position = new VideoPosition(frame, 50);
        var imageExtraction = _extractionFactory.GetImageExtraction(position);
        Assert.That(imageExtraction.Position.Frame, Is.EqualTo(frame));
    }

    [Test]
    [TestCase(20 * FrameRate, 0, 5 * FrameRate)]
    [TestCase(5 * FrameRate, 0, 5 * FrameRate)]
    [TestCase(4 * FrameRate, 0, 4 * FrameRate)]
    public void GetVideoExtractionReturnsCorrectVideoExtraction(int videoLength, int start, int expected)
    {
        var video = VideoExamples.GetVideoViewModelExample();
        var videoDuration = TimeSpan.FromSeconds((double)videoLength / 50);
        video.VideoInfo = new VideoInfo(videoDuration, 50);

        var startPosition = new VideoPosition(start, 50);
        var videoExtraction = _extractionFactory.GetVideoExtraction(startPosition, video);

        Assert.Multiple(() =>
        {
            Assert.That(videoExtraction.Begin.Position.Frame, Is.EqualTo(start));
            Assert.That(videoExtraction.End.Position.Frame, Is.EqualTo(expected));
        });
    }
}