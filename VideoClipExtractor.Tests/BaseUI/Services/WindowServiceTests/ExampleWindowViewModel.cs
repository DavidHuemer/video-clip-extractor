using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;

namespace VideoClipExtractor.Tests.BaseUI.Services.WindowServiceTests;

public class ExampleWindowViewModel(IDependencyProvider provider) : WindowViewModel(provider)
{
}