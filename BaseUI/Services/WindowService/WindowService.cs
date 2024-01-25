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

    public void ShowWindow(IWindow window)
    {
        window.Show();
    }

    public void ShowDialog(IWindow window)
    {
        window.ShowDialog();
    }

    public IWindow GetWindow<TViewModel>(TViewModel viewModel) where TViewModel : WindowViewModel
    {
        var window = _windowViewModels[viewModel.GetType()];

        if (window is null)
            throw new Exception("Window not found");

        var windowInstance = Activator.CreateInstance(window) as IWindow;

        if (windowInstance is null)
            throw new Exception("Window not found");

        windowInstance.DataContext = viewModel;
        return windowInstance;
    }
}