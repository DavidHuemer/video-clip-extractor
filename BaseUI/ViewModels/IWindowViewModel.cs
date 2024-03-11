namespace BaseUI.ViewModels;

public interface IWindowViewModel
{
    /// <summary>
    /// Shows the window.
    /// </summary>
    void Show();

    /// <summary>
    /// Shows the window as a dialog.
    /// </summary>
    void ShowDialog();
}