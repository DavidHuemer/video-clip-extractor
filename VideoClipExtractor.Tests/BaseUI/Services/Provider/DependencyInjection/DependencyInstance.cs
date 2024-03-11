using BaseUI.Services.Provider.DependencyInjection;

namespace VideoClipExtractor.Tests.BaseUI.Services.Provider.DependencyInjection;

public class DependencyInstance(IDependencyProvider provider)
{
    public IDependencyProvider Provider { get; set; } = provider;
}