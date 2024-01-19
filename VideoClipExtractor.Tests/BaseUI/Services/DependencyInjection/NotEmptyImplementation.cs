using BaseUI.Services.DependencyInjection;

namespace VideoClipExtractor.Tests.BaseUI.Services.DependencyInjection;

public class NotEmptyImplementation : ITestInterface
{
    public NotEmptyImplementation(IDependencyProvider provider)
    {
    }
}