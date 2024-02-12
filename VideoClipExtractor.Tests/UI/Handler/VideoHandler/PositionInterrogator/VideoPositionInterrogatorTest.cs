using System.ComponentModel;
using Moq;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.Tests.UI.Handler.VideoHandler.PositionInterrogator;

[TestFixture]
[TestOf(typeof(VideoPositionInterrogator))]
public class VideoPositionInterrogatorTest
{
    [SetUp]
    public void Setup()
    {
        _videoPlayer = new Mock<IVideoPlayer>();
        _videoPlayerViewModel = new Mock<IVideoPlayerViewModel>();
        _timelineNavigationViewModel = new Mock<TimelineNavigationViewModel>();
        _videoPlayerViewModel.Setup(x => x.ControlPanelViewModel.TimelineViewModel.TimelineControlViewModel
            .TimelineNavigationViewModel).Returns(_timelineNavigationViewModel.Object);

        _videoPositionDispatcher = new Mock<IVideoPositionDispatcher>();

        _videoPositionInterrogator = new VideoPositionInterrogator(_videoPlayer.Object, _videoPlayerViewModel.Object,
            _videoPositionDispatcher.Object);
    }

    private Mock<IVideoPlayer> _videoPlayer = null!;

    private Mock<IVideoPlayerViewModel> _videoPlayerViewModel = null!;
    private Mock<TimelineNavigationViewModel> _timelineNavigationViewModel = null!;

    private Mock<IVideoPositionDispatcher> _videoPositionDispatcher = null!;

    private VideoPositionInterrogator _videoPositionInterrogator = null!;

    [Test]
    public void DispatcherNotStartedAtBeginning()
    {
        _videoPositionDispatcher.Verify(x => x.Start(), Times.Never);
    }

    [Test]
    public void DispatcherNotStartedWhenMediaOpenedNotPlaying()
    {
        _videoPlayerViewModel.Setup(x => x.ControlPanelViewModel.ActionBarViewModel.VideoNavigationViewModel.PlayStatus)
            .Returns(PlayStatus.Paused);

        _videoPlayer.Raise(x => x.MediaOpened += null, EventArgs.Empty);
        _videoPositionDispatcher.Verify(x => x.Start(), Times.Never);
    }

    [Test]
    public void DispatcherNotStartWhenPlayStatusChangedButMediaNotOpened()
    {
        _videoPlayerViewModel.Setup(x => x.ControlPanelViewModel.ActionBarViewModel.VideoNavigationViewModel.PlayStatus)
            .Returns(PlayStatus.Playing);

        _videoPlayerViewModel.Raise(x => x.PropertyChanged += null,
            new PropertyChangedEventArgs(nameof(VideoNavigationViewModel.PlayStatus)));

        _videoPositionDispatcher.Verify(x => x.Start(), Times.Never);
    }

    [Test]
    public void DispatcherStartedWhenMediaOpenedPlaying()
    {
        _videoPlayerViewModel.Setup(x => x.ControlPanelViewModel.ActionBarViewModel.VideoNavigationViewModel.PlayStatus)
            .Returns(PlayStatus.Playing);

        _videoPlayer.Raise(x => x.MediaOpened += null, EventArgs.Empty);

        _videoPositionDispatcher.Verify(x => x.Start(), Times.Once);
    }

    [Test]
    public void DispatcherStartWhenPlayStatusChangedToPlaying()
    {
        _videoPlayerViewModel.Setup(x => x.ControlPanelViewModel.ActionBarViewModel.VideoNavigationViewModel.PlayStatus)
            .Returns(PlayStatus.Paused);

        _videoPlayer.Raise(x => x.MediaOpened += null, EventArgs.Empty);

        _videoPlayerViewModel.Setup(x => x.ControlPanelViewModel.ActionBarViewModel.VideoNavigationViewModel.PlayStatus)
            .Returns(PlayStatus.Playing);

        _videoPlayerViewModel.Raise(x => x.PropertyChanged += null,
            new PropertyChangedEventArgs(nameof(VideoNavigationViewModel.PlayStatus)));

        _videoPositionDispatcher.Verify(x => x.Start(), Times.Once);
    }

    [Test]
    public void DispatcherStoppedWhenPlayStatusChangedToPaused()
    {
        _videoPlayerViewModel.Setup(x => x.ControlPanelViewModel.ActionBarViewModel.VideoNavigationViewModel.PlayStatus)
            .Returns(PlayStatus.Playing);

        _videoPlayer.Raise(x => x.MediaOpened += null, EventArgs.Empty);

        _videoPlayerViewModel.Setup(x => x.ControlPanelViewModel.ActionBarViewModel.VideoNavigationViewModel.PlayStatus)
            .Returns(PlayStatus.Paused);

        _videoPlayerViewModel.Raise(x => x.PropertyChanged += null,
            new PropertyChangedEventArgs(nameof(VideoNavigationViewModel.PlayStatus)));

        _videoPositionDispatcher.Verify(x => x.Stop(), Times.Once);
    }

    [Test]
    public void ViewModelPositionUpdatedWhenDispatched()
    {
        var timespan = new TimeSpan(0, 0, 10);

        _videoPlayer.Setup(x => x.Position).Returns(timespan);


        _videoPositionDispatcher.Raise(x => x.PositionDispatched += null, EventArgs.Empty);

        Assert.That(_timelineNavigationViewModel.Object.VideoPosition,
            Is.EqualTo(new VideoPosition(timespan)));
    }
}