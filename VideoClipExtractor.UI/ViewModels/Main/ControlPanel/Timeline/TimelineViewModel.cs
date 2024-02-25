﻿using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControlPanel;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline;

[UsedImplicitly]
[Singleton]
public class TimelineViewModel : BaseViewModel, ITimelineViewModel
{
    public TimelineViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        TimelineControlPanelViewModel = viewModelProvider.Get<ITimelineControlPanelViewModel>();
        TimelineControlViewModel = viewModelProvider.Get<ITimelineControlViewModel>();
    }

    public ITimelineControlPanelViewModel TimelineControlPanelViewModel { get; set; }
    public ITimelineControlViewModel TimelineControlViewModel { get; set; }

    public VideoViewModel? Video
    {
        set => TimelineControlViewModel.VideoViewModel = value;
    }
}