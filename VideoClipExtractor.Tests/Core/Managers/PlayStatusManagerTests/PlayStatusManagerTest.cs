using Moq;
using VideoClipExtractor.Core.Managers.PlayStatusManager;
using VideoClipExtractor.Core.Services.Timeout;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Tests.Basics.BaseTests;

namespace VideoClipExtractor.Tests.Core.Managers.PlayStatusManagerTests;

[TestFixture]
[TestOf(typeof(PlayStatusManager))]
public class PlayStatusManagerTest : BaseDependencyTest
{
    private Mock<ITimeoutService> _timeoutService = null!;
    private PlayStatusManager _playStatusManager = null!;

    public override void Setup()
    {
        base.Setup();
        _timeoutService = DependencyMock.CreateMockDependency<ITimeoutService>();
        _playStatusManager = new PlayStatusManager(DependencyMock.Object);
    }

    [Test]
    public void MainPlayStatusPausedAtBeginning()
    {
        Assert.That(_playStatusManager.MainPlayStatus, Is.EqualTo(PlayStatus.Paused));
    }

    [Test]
    public void SetMainPlayStatusSetsMainPlayStatus()
    {
        _playStatusManager.SetMainPlayStatus(PlayStatus.Playing);
        Assert.That(_playStatusManager.MainPlayStatus, Is.EqualTo(PlayStatus.Playing));
    }

    [Test]
    public void SetMainPlayStatusInvokesMainPlayStatusChanged()
    {
        var invoked = false;
        _playStatusManager.PlayStatusChanged += status => { invoked = true; };
        _playStatusManager.SetMainPlayStatus(PlayStatus.Playing);
        Assert.That(invoked, Is.True);
    }

    [Test]
    public void SetMainPlayStatusToPausedCancelsTimeout()
    {
        _playStatusManager.SetMainPlayStatus(PlayStatus.Paused);
        _timeoutService.Verify(service => service.CancelTimeout(), Times.Once);
    }

    [Test]
    public void VideoPositionChangedInvokesPlayPauseEventWhenMainStatusIsPaused()
    {
        _playStatusManager.SetMainPlayStatus(PlayStatus.Paused);
        var invoked = false;
        _playStatusManager.PlayPause += (sender, args) => { invoked = true; };

        _playStatusManager.VideoPositionChanged();
        Assert.That(invoked, Is.True);
    }

    [Test]
    public void PlayStatusChangedEventInvokedWhenVideoPositionChangedAndMainStatusIsPlaying()
    {
        _playStatusManager.SetMainPlayStatus(PlayStatus.Playing);
        var invoked = false;
        _playStatusManager.PlayStatusChanged += status => { invoked = true; };
        _playStatusManager.VideoPositionChanged();
        Assert.That(invoked, Is.True);
    }

    [Test]
    public void TimeoutServiceIsCalledWhenVideoPositionChangedAndMainStatusIsPlaying()
    {
        _playStatusManager.SetMainPlayStatus(PlayStatus.Playing);
        _playStatusManager.VideoPositionChanged();
        _timeoutService.Verify(service => service.RequestTimeout(), Times.Once);
    }

    [Test]
    public void EndTimeoutCallsPlayStatusChanged()
    {
        var invoked = false;
        _playStatusManager.PlayStatusChanged += status => { invoked = true; };
        _timeoutService.Raise(service => service.EndTimeout += null!, EventArgs.Empty);
        Assert.That(invoked, Is.True);
    }
}