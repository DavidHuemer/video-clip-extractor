using FFMpeg.Wrapper.Data;
using Moq;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;
using VideoClipExtractor.Tests.Basics.Mocks;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.Tests.UI.Handler.VideoHandler.PositionInterrogator;

[TestFixture]
[TestOf(typeof(VideoPositionInterrogator))]
public class VideoPositionInterrogatorTest : BaseViewModelTest
{
    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _videoPlayer = new Mock<IVideoPlayer>();

        _videoPositionDispatcher = DependencyMock.CreateMockDependency<IVideoPositionDispatcher>();
        _videoManager = DependencyMock.CreateMockDependency<IVideoManager>();


        _videoNavigationViewModel = ViewModelProviderMock.CreateViewModelMock<IVideoNavigationViewModel>();
        _frameNavigationViewModel = ViewModelProviderMock.CreateViewModelMock<IFrameNavigationViewModel>();
        _videoPositionInterrogator = new VideoPositionInterrogator(DependencyMock.Object);
        _videoPositionInterrogator.Setup(_videoPlayer.Object);
    }

    private Mock<IVideoManager> _videoManager = null!;
    private Mock<IVideoPlayer> _videoPlayer = null!;
    private Mock<IVideoPositionDispatcher> _videoPositionDispatcher = null!;
    private ViewModelMock<IVideoNavigationViewModel> _videoNavigationViewModel = null!;
    private ViewModelMock<IFrameNavigationViewModel> _frameNavigationViewModel = null!;
    private VideoPositionInterrogator _videoPositionInterrogator = null!;

    [Test]
    public void DispatcherNotStartedAtBeginning()
    {
        _videoPositionDispatcher.Verify(x => x.Start(), Times.Never);
    }

    [Test]
    public void DispatcherStartWhenPlayStatusChangedToPlaying()
    {
        _videoNavigationViewModel.Setup(x => x.PlayStatus).Returns(PlayStatus.Playing);
        _videoNavigationViewModel.RaisePropertyChanged(nameof(VideoNavigationViewModel.PlayStatus));
        _videoPositionDispatcher.Verify(x => x.Start(), Times.Once);
    }

    [Test]
    public void DispatcherStoppedWhenPlayStatusChangedToPaused()
    {
        _videoNavigationViewModel.Setup(x => x.PlayStatus).Returns(PlayStatus.Playing);

        _videoNavigationViewModel.Setup(x => x.PlayStatus).Returns(PlayStatus.Paused);
        _videoNavigationViewModel.RaisePropertyChanged(nameof(VideoNavigationViewModel.PlayStatus));

        _videoPositionDispatcher.Verify(x => x.Stop(), Times.Once);
    }

    [Test]
    public void ViewModelPositionUpdatedWhenDispatched()
    {
        var timespan = new TimeSpan(0, 0, 10);
        _videoPlayer.Setup(x => x.Position).Returns(timespan);

        var video = VideoExamples.GetVideoViewModelExample();
        video.VideoInfo = new VideoInfo(TimeSpan.Zero, 30);
        _videoManager.Setup(x => x.Video).Returns(video);

        _videoPositionDispatcher.Raise(x
            => x.PositionDispatched += null, EventArgs.Empty);

        var expectedPosition = new VideoPosition(timespan, 30);

        _frameNavigationViewModel.VerifySet(x =>
            x.VideoPosition = expectedPosition);
    }
}