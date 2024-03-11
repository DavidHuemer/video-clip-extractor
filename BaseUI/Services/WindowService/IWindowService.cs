using BaseUI.ViewModels;

namespace BaseUI.Services.WindowService;

/// <summary>
///     Responsible for instantiating and showing windows.
/// </summary>
public interface IWindowService
{
    void Register<TViewModel, TWindow>()
        where TViewModel : WindowViewModel
        where TWindow : IWindow, new();

    void ShowWindow(IWindow window);
    void ShowDialog(IWindow window);

    IWindow GetWindow<TViewModel>(TViewModel viewModel) where TViewModel : WindowViewModel;
}