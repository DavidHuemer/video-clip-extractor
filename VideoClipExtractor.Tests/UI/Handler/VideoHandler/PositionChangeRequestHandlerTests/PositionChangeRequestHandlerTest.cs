using Moq;
using VideoClipExtractor.Core.Managers.PlayStatusManager;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionChangeRequestHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.Tests.UI.Handler.VideoHandler.PositionChangeRequestHandlerTests;

[TestFixture]
[TestOf(typeof(PositionChangeRequestHandler))]
public class PositionChangeRequestHandlerTest : BaseViewModelTest
{
    private Mock<IPlayStatusManager> _playStatusManager = null!;
    private Mock<IVideoPositionService> _videoPositionService = null!;
    private ViewModelMock<IFrameNavigationViewModel> _frameNavigationViewModel = null!;

    private Mock<IVideoPlayer> _videoPlayer = null!;

    private PositionChangeRequestHandler _positionChangeRequestHandler = null!;

    public override void Setup()
    {
        base.Setup();
        _videoPlayer = new Mock<IVideoPlayer>();

        _playStatusManager = DependencyMock.CreateMockDependency<IPlayStatusManager>();
        _videoPositionService = DependencyMock.CreateMockDependency<IVideoPositionService>();
        _frameNavigationViewModel = ViewModelProviderMock.CreateViewModelMock<IFrameNavigationViewModel>();
        _positionChangeRequestHandler = new PositionChangeRequestHandler(DependencyMock.Object);
    }


    [Test]
    public void PositionRequestedSetsFrameNavigationViewModel()
    {
        _positionChangeRequestHandler.Setup(_videoPlayer.Object);

        var videoPosition = new VideoPosition(TimeSpan.Zero, 50);
        _videoPositionService.Raise(x => x.PositionChangeRequested += null, videoPosition);

        _frameNavigationViewModel.VerifySet(x => x.VideoPosition = videoPosition);
    }

    [Test]
    public void PositionRequestedSetsVideoPlayerPosition()
    {
        _positionChangeRequestHandler.Setup(_videoPlayer.Object);

        var videoPosition = new VideoPosition(TimeSpan.Zero, 50);
        _videoPositionService.Raise(x => x.PositionChangeRequested += null, videoPosition);

        _videoPlayer.VerifySet(x => x.Position = videoPosition.Time);
    }

    [Test]
    public void PositionRequestedCallsVideoPositionChanged()
    {
        _positionChangeRequestHandler.Setup(_videoPlayer.Object);

        var videoPosition = new VideoPosition(TimeSpan.Zero, 50);
        _videoPositionService.Raise(x => x.PositionChangeRequested += null, videoPosition);

        _playStatusManager.Verify(x => x.VideoPositionChanged());
    }
}