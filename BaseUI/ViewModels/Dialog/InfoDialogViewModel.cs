using Material.Icons;

namespace BaseUI.ViewModels.Dialog;

public class InfoDialogViewModel : BaseDialogViewModel
{
    #region Properties

    public MaterialIconKind Icon { get; set; } = MaterialIconKind.Info;

    public string Message { get; set; } = "";

    #endregion
}