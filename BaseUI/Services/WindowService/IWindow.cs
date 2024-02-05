namespace BaseUI.Services.WindowService;

public interface IWindow
{
    #region Properties

    public object DataContext { get; set; }

    #endregion

    /// <summary>
    ///     Shows the window.
    /// </summary>
    void Show();

    /// <summary>
    ///     Shows the window as a dialog.
    /// </summary>
    /// <returns></returns>
    bool? ShowDialog();

    /// <summary>
    ///     Closes the window.
    /// </summary>
    void Close();

    #region Events

    /// <summary>
    ///     Occurs when the window is rendered.
    /// </summary>
    event EventHandler? ContentRendered;

    /// <summary>
    ///     Occurs when the window is closed.
    /// </summary>
    event EventHandler? Closed;

    #endregion
}