using System.Collections.ObjectModel;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineExtractions;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

[Singleton]
public class TimelineControlViewModel : BaseViewModelContainer, ITimelineControlViewModel
{
    public TimelineControlViewModel(IDependencyProvider provider) : base(provider)
    {
        Provider = provider;
        VideoNavigation = ViewModelProvider.Get<IVideoNavigationViewModel>();
        TimelineNavigationViewModel = ViewModelProvider.Get<ITimelineNavigationViewModel>();
        TimelineExtractionsViewModel = ViewModelProvider.Get<ITimelineExtractionsViewModel>();
        FrameNavigationViewModel = ViewModelProvider.Get<IFrameNavigationViewModel>();


        var frameVisualizationHandler = provider.GetDependency<IFramesVisualizationHandler>();
        frameVisualizationHandler.Setup(this);
    }

    public IVideoNavigationViewModel VideoNavigation { get; }

    public IFrameNavigationViewModel FrameNavigationViewModel { get; set; }

    public ObservableCollection<int> VerticalLines { get; } = [];
    public IDependencyProvider Provider { get; }

    public ITimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    public ITimelineExtractionsViewModel TimelineExtractionsViewModel { get; set; }

    public VideoViewModel? VideoViewModel
    {
        set => TimelineExtractionsViewModel.Video = value;
    }

    public ObservableCollection<VideoPosition> TimelineIndicators { get; set; } = [];

    public ObservableCollection<VideoPosition> TimelineSupporters { get; set; } = [];
}