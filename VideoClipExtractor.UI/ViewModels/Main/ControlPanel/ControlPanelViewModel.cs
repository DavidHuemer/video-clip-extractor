using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel;

[UsedImplicitly]
public class ControlPanelViewModel : BaseViewModel, IControlPanelViewModel
{
    public ControlPanelViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ActionBarViewModel = viewModelProvider.GetViewModel<IActionBarViewModel>();
    }

    #region Properties

    public IActionBarViewModel ActionBarViewModel { get; set; }

    #endregion
}