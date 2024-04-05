namespace BaseUI.Basics.TimerWrapper;

public interface ITimerWrapper
{
    event EventHandler? TimerEnded;
    void SetTimer(int dueTime, int period);
}