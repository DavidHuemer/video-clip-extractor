using BaseUI.Services.Provider.Attributes;

namespace BaseUI.Basics.TimerWrapper;

[Transient]
public class TimerWrapper : ITimerWrapper
{
    private readonly Timer _timer;

    public TimerWrapper()
    {
        _timer = new Timer(TimerCallback, null, Timeout.Infinite, Timeout.Infinite);
    }

    public event EventHandler? TimerEnded;

    public void SetTimer(int dueTime, int period)
    {
        _timer.Change(dueTime, period);
    }

    private void TimerCallback(object? state)
    {
        TimerEnded?.Invoke(this, EventArgs.Empty);
    }
}