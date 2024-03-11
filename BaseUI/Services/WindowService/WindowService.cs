using BaseUI.Exceptions.DependencyExceptions;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.InstanceBuilderService;
using BaseUI.Services.WindowService.ActiveWindow;
using BaseUI.ViewModels;

namespace BaseUI.Services.WindowService;

[Singleton]
public class WindowService(IDependencyProvider provider) : IWindowService
{
    private readonly IActiveWindowManager _activeWindowManager =
        provider.GetDependency<IActiveWindowManager>();

    private readonly IInstanceBuilderService _instanceBuilderService =
        provider.GetDependency<IInstanceBuilderService>();

    private readonly Dictionary<Type, Type> _windowViewModels = new();

    public void Register<TViewModel, TWindow>() where TViewModel : WindowViewModel
        where TWindow : IWindow, new()
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
        var windowType = GetWindowType(viewModel);
        var windowInstance = _instanceBuilderService.InstantiateType<IWindow>(windowType);

        windowInstance.DataContext = viewModel;
        return windowInstance;
    }

    private Type GetWindowType<TViewModel>(TViewModel viewModel) where TViewModel : WindowViewModel
    {
        if (!_windowViewModels.ContainsKey(viewModel.GetType()))
            throw new WindowNotRegisteredException();

        return _windowViewModels[viewModel.GetType()];
    }

    private void SetupActiveWindow(IWindow window) =>
        _activeWindowManager.AddWindow(window);
}