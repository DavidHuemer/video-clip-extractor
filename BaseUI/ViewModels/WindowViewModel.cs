using BaseUI.Services.WindowService;

namespace BaseUI.ViewModels;

/// <summary>
/// Base class for all window view models. 
/// </summary>
public class WindowViewModel : BaseViewModel
{
    #region Private Fields

    private IWindow? _window;

    #endregion

    #region Properties

    /// <summary>
    /// The title of the window.
    /// </summary>
    public string Title { get; set; } = "Title";

    #endregion

    public void Show(IWindowService windowService)
    {
        _window = windowService.ShowWindow(this);
    }

    public void Close()
    {
        _window?.Close();
    }
}