using BaseUI.Basics.CurrentApplicationWrapper;

namespace VideoClipExtractor.Tests.Basics.Mocks;

public class CurrentApplicationTestWrapper : ICurrentApplicationWrapper
{
    public void Run(Action action) => action();
}