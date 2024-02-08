using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControlPanel;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline;

[UsedImplicitly]
public class TimelineViewModel : BaseViewModel, ITimelineViewModel
{
    public TimelineViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        TimelineControlPanelViewModel = viewModelProvider.GetViewModel<ITimelineControlPanelViewModel>();
        TimelineControlViewModel = viewModelProvider.GetViewModel<ITimelineControlViewModel>();
    }

    public ITimelineControlPanelViewModel TimelineControlPanelViewModel { get; set; }
    public ITimelineControlViewModel TimelineControlViewModel { get; set; }
}