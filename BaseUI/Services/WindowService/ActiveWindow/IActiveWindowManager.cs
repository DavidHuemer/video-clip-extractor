namespace BaseUI.Services.WindowService.ActiveWindow;

/// <summary>
///     Responsible for managing the currently active window.
///     Automatically updates the active window when a window is closed.
/// </summary>
public interface IActiveWindowManager
{
    /// <summary>
    ///     The currently active window.
    /// </summary>
    public IWindow? ActiveWindow { get; }

    /// <summary>
    ///     Adds a window to the active window manager.
    /// </summary>
    /// <param name="window">The window that should be the next active window</param>
    void AddWindow(IWindow window);
}