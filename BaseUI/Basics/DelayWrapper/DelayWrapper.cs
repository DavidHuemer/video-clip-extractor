namespace BaseUI.Basics.DelayWrapper;

public class DelayWrapper : IDelayWrapper
{
    public void RunAfterDelay(int delay, Action action)
    {
        Task.Run(async () =>
        {
            await Task.Delay(delay);
            action();
        });
    }
}