namespace BaseUI.Basics.DelayWrapper;

public interface IDelayWrapper
{
    void RunAfterDelay(int delay, Action action);
}