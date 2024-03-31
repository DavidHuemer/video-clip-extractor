using BaseUI.Basics.DelayWrapper;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class DelayTestWrapper : IDelayWrapper
{
    public void RunAfterDelay(int delay, Action action) => action();
}