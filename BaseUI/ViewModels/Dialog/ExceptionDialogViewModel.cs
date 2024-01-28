using Material.Icons;

namespace BaseUI.ViewModels.Dialog;

public class ExceptionDialogViewModel : InfoDialogViewModel
{
    public ExceptionDialogViewModel(Exception exception)
    {
        Title = "Error";
        Icon = MaterialIconKind.Error;
        Message = exception.Message;
    }
}