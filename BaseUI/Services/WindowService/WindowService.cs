using BaseUI.Services.DependencyInjection;
using BaseUI.Services.WindowService.ActiveWindow;
using BaseUI.ViewModels;
using JetBrains.Annotations;

namespace BaseUI.Services.WindowService;

[UsedImplicitly]
public class WindowService(IDependencyProvider provider) : IWindowService
{
    private readonly Dictionary<Type, Type> _windowViewModels = new();

    public void Register<TViewModel, TWindow>() where TViewModel : WindowViewModel
        where TWindow : Window, IWindow, new()
    {
        _windowViewModels.Add(typeof(TViewModel), typeof(TWindow));
    }

    public void ShowWindow(IWindow window)
    {
        SetupActiveWindow(window);
        window.Show();
    }

    public void ShowDialog(IWindow window)
    {
        SetupActiveWindow(window);
        window.ShowDialog();
    }

    public IWindow GetWindow<TViewModel>(TViewModel viewModel) where TViewModel : WindowViewModel
    {
        var window = _windowViewModels[viewModel.GetType()];

        if (window is null)
            throw new Exception("Window not found");

        if (Activator.CreateInstance(window) is not IWindow windowInstance)
            throw new Exception("Window not found");

        windowInstance.DataContext = viewModel;
        return windowInstance;
    }

    private void SetupActiveWindow(IWindow window)
    {
        provider.GetDependency<IActiveWindowManager>().AddWindow(window);
    }
}