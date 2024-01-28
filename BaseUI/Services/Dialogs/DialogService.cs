using BaseUI.Dialogs;
using BaseUI.ViewModels.Dialog;
using JetBrains.Annotations;
using MaterialDesignThemes.Wpf;

namespace BaseUI.Services.Dialogs;

[UsedImplicitly]
public class DialogService : IDialogService
{
    public void Show(Exception exception)
    {
        var exceptionVm = new ExceptionDialogViewModel(exception);
        ShowInfo(exceptionVm);
    }

    private static void ShowInfo(InfoDialogViewModel vm)
    {
        var infoDialog = new InfoDialog
        {
            DataContext = vm,
        };
        DialogHost.Show(infoDialog);
    }
}