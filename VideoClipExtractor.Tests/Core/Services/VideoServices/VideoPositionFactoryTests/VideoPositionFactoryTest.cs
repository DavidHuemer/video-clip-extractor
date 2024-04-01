using FFMpeg.Wrapper.Data;
using Moq;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionFactory;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Services.VideoServices.VideoPositionFactoryTests;

[TestFixture]
[TestOf(typeof(VideoPositionFactory))]
public class VideoPositionFactoryTest : BaseDependencyTest
{
    private Mock<IVideoManager> _videoManager = null!;

    private VideoPositionFactory _videoPositionFactory = null!;

    public override void Setup()
    {
        base.Setup();
        _videoManager = DependencyMock.CreateMockDependency<IVideoManager>();
        _videoPositionFactory = new VideoPositionFactory(DependencyMock.Object);
    }


    [Test]
    [TestCase(0, 30, "00:00:00:00")]
    [TestCase(30, 30, "00:00:01:00")]
    [TestCase(34, 30, "00:00:01:04")]
    [TestCase(50, 50, "00:00:01:00")]
    public void GetVideoPositionByFrameReturnsCorrectVideoPosition(int frame, double frameRate, string expected)
    {
        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoInfo = new VideoInfo(TimeSpan.Zero, frameRate);
        _videoManager.SetupGet(x => x.Video).Returns(video);

        var videoPosition = _videoPositionFactory.GetVideoPositionByFrame(frame);
        Assert.That(videoPosition.ToString(), Is.EqualTo(expected));
    }

    [Test]
    [TestCase("00:00:00:00", 30, "00:00:00:00")]
    [TestCase("00:00:00:01", 30, "00:00:00:01")]
    [TestCase("00:00:00:05", 30, "00:00:00:05")]
    [TestCase("00:00:00:05", 50, "00:00:00:05")]
    [TestCase("00:00:02:05", 50, "00:00:02:05")]
    [TestCase("00:00:00:50", 50, "00:00:01:00")]
    [TestCase("00:00:00:500", 50, "00:00:10:00")]
    public void GetVideoPositionByStringReturnsCorrectVideoPosition(string input, double frameRate, string expected)
    {
        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoInfo = new VideoInfo(TimeSpan.Zero, frameRate);
        _videoManager.SetupGet(x => x.Video).Returns(video);

        var videoPosition = _videoPositionFactory.GetVideoPositionByString(input);
        Assert.That(videoPosition.ToString(), Is.EqualTo(expected));
    }

    [Test]
    [TestCase("")]
    [TestCase("00")]
    [TestCase("00:00")]
    [TestCase("00:00:00")]
    [TestCase("00:00:00:00:")]
    [TestCase("0a:00:00:00")]
    public void GetVideoPositionByStringThrowsArgumentExceptionWhenInputIsInvalid(string input)
    {
        Assert.Throws<ArgumentException>(() => _videoPositionFactory.GetVideoPositionByString(input));
    }
}