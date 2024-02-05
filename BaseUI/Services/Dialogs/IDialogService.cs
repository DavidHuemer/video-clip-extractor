namespace BaseUI.Services.Dialogs;

public interface IDialogService
{
    /// <summary>
    ///     Shows a dialog based on the given exception
    /// </summary>
    /// <param name="exception">The exception that should be displayed</param>
    void Show(Exception exception);
}