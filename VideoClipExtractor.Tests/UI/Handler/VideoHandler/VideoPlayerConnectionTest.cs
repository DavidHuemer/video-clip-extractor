using Moq;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.Events;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.Tests.UI.Handler.VideoHandler;

[TestFixture]
[TestOf(typeof(VideoPlayerConnection))]
public class VideoPlayerConnectionTest : BaseDependencyTest
{
    private Mock<IVideoPlayer> _videoPlayerMock = null!;
    private Mock<IVideoPlayerViewModel> _videoPlayerViewModelMock = null!;
    private Mock<IVideoNavigationViewModel> _videoNavigationViewModelMock = null!;
    private Mock<IVideoPositionInterrogator> _videoPositionInterrogatorMock = null!;
    private Mock<IVideoPositionService> _videoPositionServiceMock = null!;

    private VideoPlayerConnection _videoPlayerConnection = null!;

    public override void Setup()
    {
        base.Setup();
        _videoPlayerMock = new Mock<IVideoPlayer>();

        _videoPlayerViewModelMock = new Mock<IVideoPlayerViewModel>();
        _videoPlayerViewModelMock.SetupGet(x => x.Provider).Returns(DependencyMock.Object);

        var viewModelProviderMock = DependencyMock.AddViewModelProvider();
        _videoNavigationViewModelMock = viewModelProviderMock.CreateViewModelMock<IVideoNavigationViewModel>();
        _videoPositionInterrogatorMock = DependencyMock.CreateMockDependency<IVideoPositionInterrogator>();
        _videoPositionServiceMock = DependencyMock.CreateMockDependency<IVideoPositionService>();

        _videoPlayerConnection = new VideoPlayerConnection(_videoPlayerMock.Object, _videoPlayerViewModelMock.Object);
    }

    [Test]
    public void VideoPositionInterrogatorIsSetup()
    {
        _videoPositionInterrogatorMock
            .Verify(x => x.Setup(_videoPlayerMock.Object), Times.Once);
    }

    [Test]
    public void VideoPlayerPositionIsChanged()
    {
        var videoPosition = new VideoPosition(15);

        _videoPositionServiceMock.Raise(x => x.PositionChangeRequested += null,
            new VideoPositionEventArgs(videoPosition));

        _videoPlayerMock.VerifySet(x => x.Position = videoPosition.Duration.TimeSpan, Times.Once);
    }

    [Test]
    public void VideoPositionIsChanged()
    {
        var videoPosition = new VideoPosition(15);

        _videoPositionServiceMock.Raise(x => x.PositionChangeRequested += null,
            new VideoPositionEventArgs(videoPosition));

        _videoNavigationViewModelMock.VerifySet(x => x.VideoPosition = videoPosition, Times.Once);
    }
}