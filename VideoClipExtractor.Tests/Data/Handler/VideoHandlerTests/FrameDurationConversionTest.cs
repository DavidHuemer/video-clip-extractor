using VideoClipExtractor.Data.Handler.Video;

namespace VideoClipExtractor.Tests.Data.Handler.VideoHandlerTests;

[TestFixture]
[TestOf(typeof(FrameDurationConversion))]
public class FrameDurationConversionTest
{
    [Test]
    [TestCase(0, 30, 0)]
    [TestCase(30, 30, 1000)]
    [TestCase(15, 30, 500)]
    [TestCase(60, 30, 2000)]
    public void GetDurationByFrameReturnsCorrectDuration(int frame, int framerate, int expected)
    {
        var result = FrameDurationConversion.GetDurationByFrame(frame, framerate);
        Assert.AreEqual(expected, result.TotalMilliseconds);
    }

    [Test]
    [TestCase(1000, 30, 30)]
    [TestCase(0, 30, 0)]
    [TestCase(2000, 30, 60)]
    [TestCase(500, 30, 15)]
    public void GetFrameByTimespanReturnsCorrectFrame(int milliseconds, int framerate, int expected)
    {
        var time = TimeSpan.FromMilliseconds(milliseconds);
        var result = FrameDurationConversion.GetFrameByTimespan(time, framerate);
        Assert.AreEqual(expected, result);
    }
}