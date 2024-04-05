using BaseUI.Basics.TimerWrapper;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;

namespace VideoClipExtractor.Core.Services.Timeout;

[Transient]
public class TimeoutService : ITimeoutService
{
    public const int TimeoutTime = 250;

    private readonly ITimerWrapper _timerWrapper;

    public TimeoutService(IDependencyProvider provider)
    {
        _timerWrapper = provider.GetDependency<ITimerWrapper>();
        _timerWrapper.TimerEnded += (_, _) => EndTimeout?.Invoke(this, EventArgs.Empty);
    }

    public event EventHandler? EndTimeout;

    public void RequestTimeout()
    {
        _timerWrapper.SetTimer(TimeoutTime, System.Threading.Timeout.Infinite);
    }

    public void CancelTimeout()
    {
        _timerWrapper.SetTimer(System.Threading.Timeout.Infinite, System.Threading.Timeout.Infinite);
    }
}