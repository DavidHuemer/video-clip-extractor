using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel;

[UsedImplicitly]
[Singleton]
public class ControlPanelViewModel : BaseViewModel, IControlPanelViewModel
{
    public ControlPanelViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ActionBarViewModel = viewModelProvider.Get<IActionBarViewModel>();
        TimelineViewModel = viewModelProvider.Get<ITimelineViewModel>();
    }

    #region Properties

    public IActionBarViewModel ActionBarViewModel { get; set; }
    public ITimelineViewModel TimelineViewModel { get; set; }

    public VideoViewModel? Video
    {
        set
        {
            ActionBarViewModel.Video = value;
            TimelineViewModel.Video = value;
        }
    }

    #endregion
}