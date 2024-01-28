using System.Windows.Input;
using BaseUI.Commands;
using MaterialDesignThemes.Wpf;

namespace BaseUI.ViewModels.Dialog;

/// <summary>
/// Base class for all dialog view models
/// </summary>
public abstract class BaseDialogViewModel : BaseViewModel
{
    #region Properties

    public string Title { get; protected init; } = "Title";

    #endregion

    #region Commands

    public static ICommand Close => new RelayCommand<string>(DoClose, _ => true);

    private static void DoClose(string? obj) =>
        DialogHost.CloseDialogCommand.Execute(null, null);

    #endregion
}