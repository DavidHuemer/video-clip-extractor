namespace BaseUI.Services.Dialogs.Identifier;

public interface IDialogHostIdentifierService
{
    /// <summary>
    /// Returns the identifier of the dialog host in the active window.
    /// </summary>
    /// <returns>The identifier of the dialog host in the active window</returns>
    string GetIdentifier();
}