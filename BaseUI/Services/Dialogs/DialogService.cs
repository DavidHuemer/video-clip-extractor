using BaseUI.Dialogs;
using BaseUI.Services.DependencyInjection;
using BaseUI.Services.Dialogs.Identifier;
using BaseUI.ViewModels.Dialog;
using JetBrains.Annotations;
using MaterialDesignThemes.Wpf;
using static System.String;

namespace BaseUI.Services.Dialogs;

[UsedImplicitly]
internal class DialogService(IDependencyProvider provider) : IDialogService
{
    public void Show(Exception exception)
    {
        var exceptionVm = new ExceptionDialogViewModel(exception);
        ShowInfo(exceptionVm);
    }

    private void ShowInfo(InfoDialogViewModel vm)
    {
        var infoDialog = new InfoDialog
        {
            DataContext = vm,
        };

        ShowDialog(infoDialog);
    }

    private void ShowDialog(FrameworkElement content)
    {
        var identifier = GetIdentifier();

        if (identifier == Empty)
        {
            DialogHost.Show(content);
        }
        else
        {
            DialogHost.Show(content, identifier);
        }
    }

    private string GetIdentifier() =>
        provider.GetDependency<IDialogHostIdentifierService>().GetIdentifier();
}