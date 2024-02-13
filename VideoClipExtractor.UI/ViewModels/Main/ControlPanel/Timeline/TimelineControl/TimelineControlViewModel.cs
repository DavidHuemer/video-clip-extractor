using System.Collections.ObjectModel;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

[UsedImplicitly]
public class TimelineControlViewModel : BaseViewModel, ITimelineControlViewModel
{
    // public TimelineControlViewModel(IDependencyProvider provider,
    //     IFramesVisualizationHandler? framesVisualizationHandler = null,
    //     ITimelineFrameWidthHandler? timelineFrameWidthHandler = null)
    // {
    //     var viewModelProvider = provider.GetDependency<IViewModelProvider>();
    //     VideoNavigation = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();
    //
    //     _timelineFrameWidthHandler = timelineFrameWidthHandler ?? new TimelineFrameWidthHandler();
    //     TimelineNavigationViewModel = viewModelProvider.GetViewModel<ITimelineNavigationViewModel>();
    //
    //     // framesVisualizationHandler ??= new FrameVisualizationHandler(_timelineFrameWidthHandler);
    //     // framesVisualizationHandler.Setup(this);
    // }

    public TimelineControlViewModel(IDependencyProvider provider)
    {
        Provider = provider;
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        VideoNavigation = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();
        TimelineNavigationViewModel = viewModelProvider.GetViewModel<ITimelineNavigationViewModel>();

        var frameVisualizationHandler = provider.GetDependency<IFramesVisualizationHandler>();
        frameVisualizationHandler.Setup(this);
    }

    public IVideoNavigationViewModel VideoNavigation { get; set; }

    public ObservableCollection<int> VerticalLines { get; } = [];
    public IDependencyProvider Provider { get; }

    public ITimelineNavigationViewModel TimelineNavigationViewModel { get; set; }

    public void PauseVideo()
    {
        VideoNavigation.PlayStatus = PlayStatus.Paused;
        VideoNavigation.PlayStatus = PlayStatus.Playing;
        VideoNavigation.PlayStatus = PlayStatus.Paused;
    }
}