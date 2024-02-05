using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.Tests.Basics.Data;

namespace VideoClipExtractor.Tests.Core.Managers.VideoManagerTests;

public class VideoManagerTests
{
    private VideoManager _videoManager = null!;

    [SetUp]
    public void Setup()
    {
        _videoManager = new VideoManager();
    }

    [Test]
    public void VideoInvoked_VideoChangedEvent()
    {
        // Arrange
        var video = VideoExamples.GetVideoExample();
        VideoChangedEventArgs? videoChangedEventArgs = null;
        _videoManager.VideoChanged += (_, args) => videoChangedEventArgs = args;

        // Act
        _videoManager.Video = video;

        // Assert
        Assert.That(videoChangedEventArgs?.Video, Is.EqualTo(video));
    }
}