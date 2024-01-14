using System.Windows;
using BaseUI.ViewModels;

namespace BaseUI.Services.WindowService;

public interface IWindowService
{
    void Register<TViewModel, TWindow>()
        where TViewModel : WindowViewModel
        where TWindow : Window, IWindow, new();

    IWindow ShowWindow<TViewModel>(TViewModel viewModel) where TViewModel : WindowViewModel;
}