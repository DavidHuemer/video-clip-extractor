using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.WindowService;

namespace BaseUI.ViewModels;

/// <summary>
///     Base class for all window view models.
/// </summary>
public class WindowViewModel : BaseViewModel, IWindowViewModel
{
    public WindowViewModel(IDependencyProvider provider)
    {
        _windowService = provider.GetDependency<IWindowService>();
    }

    #region Properties

    /// <summary>
    ///     The title of the window.
    /// </summary>
    public string Title { get; set; } = "Title";

    #endregion

    public void ShowDialog()
    {
        _window = GetWindow(_windowService);
        _windowService.ShowDialog(_window);
    }

    /// <summary>
    ///     Shows the window.
    /// </summary>
    /// <param name="windowService">The <see cref="IWindowService" /> that is responsible for instantiating the window</param>
    public void Show(IWindowService windowService)
    {
        _window = GetWindow(windowService);
        windowService.ShowWindow(_window);
        SetupEvents(_window);
    }

    /// <summary>
    ///     Shows the window as a dialog.
    /// </summary>
    /// <param name="windowService">The <see cref="IWindowService" /> that is responsible for instantiating the window</param>
    public void ShowDialog(IWindowService windowService)
    {
        _window = GetWindow(windowService);
        windowService.ShowDialog(_window);
        SetupEvents(_window);
    }

    private IWindow GetWindow(IWindowService windowService)
    {
        return windowService.GetWindow(this);
    }

    protected virtual void SetupEvents(IWindow window)
    {
    }

    #region Private Fields

    private IWindow? _window;

    private readonly IWindowService _windowService;

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