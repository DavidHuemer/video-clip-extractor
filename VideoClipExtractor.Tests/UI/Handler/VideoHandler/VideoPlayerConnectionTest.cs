using Moq;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionChangeRequestHandler;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.Tests.UI.Handler.VideoHandler;

[TestFixture]
[TestOf(typeof(VideoPlayerConnection))]
public class VideoPlayerConnectionTest : BaseViewModelTest
{
    private Mock<IVideoPlayer> _videoPlayerMock = null!;
    private Mock<IVideoPlayerViewModel> _videoPlayerViewModelMock = null!;
    private Mock<IFrameNavigationViewModel> _frameNavigationViewModelMock = null!;
    private Mock<IVideoPositionInterrogator> _videoPositionInterrogatorMock = null!;
    private Mock<IPositionChangeRequestHandler> _positionChangeRequestHandlerMock = null!;
    private Mock<IVideoPositionService> _videoPositionServiceMock = null!;

    private VideoPlayerConnection _videoPlayerConnection = null!;

    public override void Setup()
    {
        base.Setup();
        _videoPlayerMock = new Mock<IVideoPlayer>();

        _videoPlayerViewModelMock = new Mock<IVideoPlayerViewModel>();
        _videoPlayerViewModelMock.SetupGet(x => x.DependencyProvider).Returns(DependencyMock.Object);

        _frameNavigationViewModelMock = ViewModelProviderMock.CreateViewModelMock<IFrameNavigationViewModel>();
        _videoPositionInterrogatorMock = DependencyMock.CreateMockDependency<IVideoPositionInterrogator>();
        _positionChangeRequestHandlerMock = DependencyMock.CreateMockDependency<IPositionChangeRequestHandler>();
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
    public void PositionChangeRequestHandlerIsSetup()
    {
        _positionChangeRequestHandlerMock
            .Verify(x => x.Setup(_videoPlayerMock.Object), Times.Once);
    }

    // [Test]
    // public void VideoPlayerPositionIsChanged()
    // {
    //     var videoPosition = new VideoPosition(TimeSpan.Zero, 30);
    //     _videoPositionServiceMock.Raise(x => x.PositionChangeRequested += null,
    //         videoPosition);
    //     _videoPlayerMock.VerifySet(x => x.Position = videoPosition.Time, Times.Once);
    // }
    //
    // [Test]
    // public void VideoPositionIsChanged()
    // {
    //     var videoPosition = new VideoPosition1(15);
    //
    //     _videoPositionServiceMock.Raise(x => x.PositionChangeRequested += null,
    //         videoPosition);
    //
    //     _frameNavigationViewModelMock.VerifySet(x => x.VideoPosition1 = videoPosition, Times.Once);
    // }
}