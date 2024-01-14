using System.Windows;
using BaseUI.ViewModels;

namespace BaseUI.Services.WindowService;

public class WindowService : IWindowService
{
    private readonly Dictionary<Type, Type> _windowViewModels = new();

    public void Register<TViewModel, TWindow>() where TViewModel : WindowViewModel
        where TWindow : Window, IWindow, new()
    {
        _windowViewModels.Add(typeof(TViewModel), typeof(TWindow));
    }

    public IWindow ShowWindow<TViewModel>(TViewModel viewModel) where TViewModel : WindowViewModel
    {
        var window = GetWindowInstance(viewModel);
        window.Show();
        return (window as IWindow)!;
    }

    public IWindow ShowDialog<TViewModel>(TViewModel viewModel) where TViewModel : WindowViewModel
    {
        var window = GetWindowInstance(viewModel);
        window.ShowDialog();
        return (window as IWindow)!;
    }

    private Window GetWindowInstance<TViewModel>(TViewModel viewModel) where TViewModel : WindowViewModel
    {
        var window = _windowViewModels[viewModel.GetType()];

        if (window is null)
            throw new Exception("Window not found");

        var windowInstance = Activator.CreateInstance(window) as Window;

        if (windowInstance is null)
            throw new Exception("Window not found");

        windowInstance.DataContext = viewModel;
        return windowInstance;
    }
}