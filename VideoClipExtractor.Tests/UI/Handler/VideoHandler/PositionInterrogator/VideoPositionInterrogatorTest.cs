using System.ComponentModel;
using Moq;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.Tests.UI.Handler.VideoHandler.PositionInterrogator;

[TestFixture]
[TestOf(typeof(VideoPositionInterrogator))]
public class VideoPositionInterrogatorTest : BaseDependencyTest
{
    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _videoPlayer = new Mock<IVideoPlayer>();

        _videoPositionDispatcher = DependencyMock.CreateMockDependency<IVideoPositionDispatcher>();
        var viewModelProvider = DependencyMock.AddViewModelProvider();
        _videoNavigationViewModel = viewModelProvider.CreateViewModelMock<IVideoNavigationViewModel>();
        _videoPositionInterrogator = new VideoPositionInterrogator(DependencyMock.Object);
        _videoPositionInterrogator.Setup(_videoPlayer.Object);
    }

    private Mock<IVideoPlayer> _videoPlayer = null!;
    private Mock<IVideoPositionDispatcher> _videoPositionDispatcher = null!;
    private Mock<IVideoNavigationViewModel> _videoNavigationViewModel = null!;
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

        _videoNavigationViewModel.Raise(x => x.PropertyChanged += null,
            new PropertyChangedEventArgs(nameof(VideoNavigationViewModel.PlayStatus)));

        _videoPositionDispatcher.Verify(x => x.Start(), Times.Once);
    }

    [Test]
    public void DispatcherStoppedWhenPlayStatusChangedToPaused()
    {
        _videoNavigationViewModel.Setup(x => x.PlayStatus).Returns(PlayStatus.Playing);

        _videoNavigationViewModel.Setup(x => x.PlayStatus).Returns(PlayStatus.Paused);
        _videoNavigationViewModel.Raise(x => x.PropertyChanged += null,
            new PropertyChangedEventArgs(nameof(VideoNavigationViewModel.PlayStatus)));

        _videoPositionDispatcher.Verify(x => x.Stop(), Times.Once);
    }

    [Test]
    public void ViewModelPositionUpdatedWhenDispatched()
    {
        var timespan = new TimeSpan(0, 0, 10);

        _videoPlayer.Setup(x => x.Position).Returns(timespan);

        _videoPositionDispatcher.Raise(x
            => x.PositionDispatched += null, EventArgs.Empty);

        _videoNavigationViewModel.VerifySet(x =>
            x.VideoPosition = It.Is<VideoPosition>(y => y.Duration.TimeSpan == timespan));
    }
}