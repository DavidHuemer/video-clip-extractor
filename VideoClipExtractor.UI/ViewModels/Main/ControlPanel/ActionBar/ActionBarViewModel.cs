using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar;

[UsedImplicitly]
public class ActionBarViewModel : IActionBarViewModel
{
    public ActionBarViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        VideoNavigationViewModel = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();
    }

    #region Properties

    public IVideoNavigationViewModel VideoNavigationViewModel { get; set; }

    public VideoViewModel? Video
    {
        set => VideoNavigationViewModel.Video = value;
    }

    #endregion
}