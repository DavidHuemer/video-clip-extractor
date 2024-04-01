using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Core.Managers.VideoManagerTests;

[TestFixture]
[TestOf(typeof(VideoManager))]
public class VideoManagerTest
{
    [SetUp]
    public void Setup()
    {
        _videoManager = new VideoManager();
    }

    private VideoManager _videoManager = null!;

    [Test]
    public void VideoIsNullAtBeginning()
    {
        Assert.That(_videoManager.Video, Is.Null);
    }

    [Test]
    public void SettingVideoInvokesEvent()
    {
        var video = VideoExamples.GetVideoViewModelExample();
        var invoked = false;
        _videoManager.VideoChanged += (v) =>
        {
            Assert.That(v, Is.EqualTo(video));
            invoked = true;
        };
        _videoManager.Video = video;
        Assert.That(invoked, Is.True);
    }
}