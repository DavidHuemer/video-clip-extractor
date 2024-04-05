using BaseUI.Basics.TimerWrapper;
using Moq;
using VideoClipExtractor.Core.Services.Timeout;
using VideoClipExtractor.Tests.Basics.BaseTests;

namespace VideoClipExtractor.Tests.Core.Services.Timeout;

[TestFixture]
[TestOf(typeof(TimeoutService))]
public class TimeoutServiceTest : BaseDependencyTest
{
    [SetUp]
    public override void Setup()
    {
        base.Setup();
        _timerWrapper = DependencyMock.CreateMockDependency<ITimerWrapper>();
        _timeoutService = new TimeoutService(DependencyMock.Object);
    }

    private Mock<ITimerWrapper> _timerWrapper = null!;
    private TimeoutService _timeoutService = null!;


    [Test]
    public void RequestTimeoutCallsTimeWrapper()
    {
        _timeoutService.RequestTimeout();
        _timerWrapper.Verify(wrapper =>
            wrapper.SetTimer(TimeoutService.TimeoutTime, System.Threading.Timeout.Infinite));
    }

    [Test]
    public void CancelTimeoutCallsTimeWrapper()
    {
        _timeoutService.CancelTimeout();
        _timerWrapper.Verify(wrapper =>
            wrapper.SetTimer(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite));
    }

    [Test]
    public void TimerEndedInvokesEndTimeout()
    {
        var invoked = false;
        _timeoutService.EndTimeout += (sender, args) => { invoked = true; };
        _timerWrapper.Raise(wrapper => wrapper.TimerEnded += null, this, System.EventArgs.Empty);
        Assert.That(invoked, Is.True);
    }
}