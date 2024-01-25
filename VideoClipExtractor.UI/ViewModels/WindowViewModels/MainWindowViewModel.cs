using BaseUI.Services.DependencyInjection;
using BaseUI.Services.WindowService;
using BaseUI.ViewModels;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

public class MainWindowViewModel : WindowViewModel
{
    #region Private Fields

    private readonly IDependencyProvider _provider;

    #endregion

    public MainWindowViewModel(IDependencyProvider provider)
    {
        _provider = provider;
    }

    protected override void SetupEvents(IWindow window)
    {
        base.SetupEvents(window);
        window.ContentRendered += WindowOnContentRendered;
    }

    private void WindowOnContentRendered(object? sender, EventArgs e)
    {
        var windowService = _provider.GetDependency<IWindowService>();
        new WelcomeWindowViewModel(_provider).ShowDialog(windowService);
    }
}