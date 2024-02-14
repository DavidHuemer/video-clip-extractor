using System.Collections.ObjectModel;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineExtractions;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

[UsedImplicitly]
public class TimelineControlViewModel : BaseViewModel, ITimelineControlViewModel
{
    public TimelineControlViewModel(IDependencyProvider provider)
    {
        Provider = provider;
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        VideoNavigation = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();
        TimelineNavigationViewModel = viewModelProvider.GetViewModel<ITimelineNavigationViewModel>();
        TimelineExtractionsViewModel = viewModelProvider.GetViewModel<ITimelineExtractionsViewModel>();


        var frameVisualizationHandler = provider.GetDependency<IFramesVisualizationHandler>();
        frameVisualizationHandler.Setup(this);
    }

    public IVideoNavigationViewModel VideoNavigation { get; set; }

    public ObservableCollection<int> VerticalLines { get; } = [];
    public IDependencyProvider Provider { get; }

    public ITimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    public ITimelineExtractionsViewModel TimelineExtractionsViewModel { get; set; }

    public VideoViewModel? VideoViewModel
    {
        set => TimelineExtractionsViewModel.Video = value;
    }

    public void PauseVideo()
    {
        VideoNavigation.PlayStatus = PlayStatus.Paused;
        VideoNavigation.PlayStatus = PlayStatus.Playing;
        VideoNavigation.PlayStatus = PlayStatus.Paused;
    }
}