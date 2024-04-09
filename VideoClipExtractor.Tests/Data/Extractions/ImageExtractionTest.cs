using VideoClipExtractor.Data.Extractions;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Tests.Data.Extractions;

[TestFixture]
[TestOf(typeof(ImageExtraction))]
public class ImageExtractionTest
{
    [Test]
    [TestCase(0, 0, true)]
    [TestCase(1, 1, true)]
    [TestCase(500, 500, true)]
    [TestCase(0, 1, false)]
    public void TwoImageExtractionsAreEqual(int frame1, int frame2, bool shouldEqual)
    {
        var extraction1 = new ImageExtraction(new VideoPosition(frame1, 50));
        var extraction2 = new ImageExtraction(new VideoPosition(frame2, 50));

        Assert.That(extraction2, shouldEqual
            ? Is.EqualTo(extraction1)
            : Is.Not.EqualTo(extraction1));
    }
}