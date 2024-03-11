using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.WindowService;

namespace BaseUI.ViewModels;

/// <summary>
///     Base class for all window view models.
/// </summary>
public abstract class WindowViewModel(IDependencyProvider provider) : BaseViewModelContainer(provider), IWindowViewModel
{
    #region Properties

    /// <summary>
    ///     The title of the window.
    /// </summary>
    public string Title { get; set; } = "Title";

    #endregion

    public void Show()
    {
        _window = GetWindow();
        SetupEvents(_window);
        _windowService.ShowWindow(_window);
    }

    public void ShowDialog()
    {
        _window = GetWindow();
        SetupEvents(_window);
        _windowService.ShowDialog(_window);
    }

    private IWindow GetWindow() => _windowService.GetWindow(this);

    protected virtual void SetupEvents(IWindow window)
    {
    }

    #region Private Fields

    private IWindow? _window;

    private readonly IWindowService _windowService = provider.GetDependency<IWindowService>();

    #endregion

    #region Commands

    public ICommand Close => new RelayCommand<string>(DoClose, _ => true);

    private void DoClose(string? obj)
    {
        CloseWindow();
    }


    public void CloseWindow()
    {
        _window?.Close();
    }

    #endregion
}