using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Tests.Data.UI.Video;

[TestFixture]
[TestOf(typeof(VideoPosition))]
public class VideoPositionTest
{
    [Test]
    [TestCase(0, 0, 0, 0, 50, "00:00:00:00")]
    [TestCase(0, 0, 0, 0, 25, "00:00:00:00")]
    [TestCase(0, 0, 1, 0, 50, "00:00:01:00")]
    [TestCase(0, 0, 58, 50, 50, "00:00:58:25")]
    [TestCase(0, 0, 58, 50, 49.99, "00:00:58:25")]
    [TestCase(0, 0, 58, 50, 49.50, "00:00:58:25")]
    [TestCase(0, 0, 58, 50, 49.49, "00:00:58:25")]
    [TestCase(0, 0, 58, 0, 50, "00:00:58:00")]
    public void VideoPositionHasCorrectFormat(int hours, int minutes, int seconds, int ms, double frameRate,
        string expected)
    {
        var timeSpan = new TimeSpan(0, hours, minutes, seconds, ms * 10);
        var videoPosition = new VideoPosition(timeSpan, frameRate);
        Assert.That(videoPosition.ToString(), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(0, 50, "00:00:00:00")]
    [TestCase(50, 50, "00:00:01:00")]
    [TestCase(55, 50, "00:00:01:05")]
    public void VideoPositionWithFramesHasCorrectFormat(int frame, double frameRate, string expected)
    {
        var videoPosition = new VideoPosition(frame, frameRate);
        Assert.That(videoPosition.ToString(), Is.EqualTo(expected));
    }

    [Test]
    [TestCase(0, 0, 0, 0, 50, 0)]
    [TestCase(0, 0, 0, 0, 25, 0)]
    [TestCase(0, 0, 1, 0, 50, 50)]
    [TestCase(0, 0, 1, 50, 50, 75)]
    public void FrameIsReturnedCorrectly(int hours, int minutes, int seconds, int ms, double frameRate, int expected)
    {
        var timeSpan = new TimeSpan(0, hours, minutes, seconds, ms * 10);
        var videoPosition = new VideoPosition(timeSpan, frameRate);
        Assert.That(videoPosition.Frame, Is.EqualTo(expected));
    }
}