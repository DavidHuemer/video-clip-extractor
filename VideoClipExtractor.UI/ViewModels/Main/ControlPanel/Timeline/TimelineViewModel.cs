using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControlPanel;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline;

[Singleton]
public class TimelineViewModel : BaseViewModelContainer, ITimelineViewModel
{
    public TimelineViewModel(IDependencyProvider provider) : base(provider)
    {
        TimelineControlPanelViewModel = ViewModelProvider.Get<ITimelineControlPanelViewModel>();
        TimelineControlViewModel = ViewModelProvider.Get<ITimelineControlViewModel>();
    }

    public ITimelineControlPanelViewModel TimelineControlPanelViewModel { get; }
    public ITimelineControlViewModel TimelineControlViewModel { get; }

    public VideoViewModel? Video
    {
        set => TimelineControlViewModel.VideoViewModel = value;
    }
}