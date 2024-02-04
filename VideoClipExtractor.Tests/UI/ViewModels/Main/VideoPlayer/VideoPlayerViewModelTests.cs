using Moq;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.Tests.Basics.Data;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.Tests.UI.ViewModels.Main.VideoPlayer;

public class VideoPlayerViewModelTests
{
    private DependencyMock _dependencyMock = null!;
    private Mock<IVideoManager> _videoManagerMock = null!;

    private VideoPlayerViewModel _viewModel = null!;

    [SetUp]
    public void Setup()
    {
        _dependencyMock = new DependencyMock();
        _videoManagerMock = new Mock<IVideoManager>();
        _dependencyMock.AddMockDependency(_videoManagerMock);
        _viewModel = new VideoPlayerViewModel(_dependencyMock.Object);
    }

    [Test]
    public void VideoIsChanged()
    {
        var video = VideoExamples.GetVideoExample();
        _videoManagerMock.Raise(m => m.VideoChanged += null!, new VideoChangedEventArgs(video));
        Assert.That(_viewModel.Video, Is.EqualTo(video));
    }
}