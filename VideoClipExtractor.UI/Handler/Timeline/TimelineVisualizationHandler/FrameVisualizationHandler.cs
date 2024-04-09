using System.Collections.ObjectModel;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicatorsUpdateListener;
using VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimelineIndicatorsFactory;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler;

[Transient]
public class FrameVisualizationHandler(IDependencyProvider provider) : IFramesVisualizationHandler
{
    private readonly ITimelineIndicatorsFactory _timelineIndicatorsFactory =
        provider.GetDependency<ITimelineIndicatorsFactory>();

    private readonly ITimelineIndicatorsUpdateListener _timelineIndicatorsUpdateListener =
        provider.GetDependency<ITimelineIndicatorsUpdateListener>();

    public void Setup(ITimelineControlViewModel timelineControlViewModel)
    {
        _timelineIndicatorsUpdateListener.Setup(timelineControlViewModel);
        _timelineIndicatorsUpdateListener.TimelineIndicatorsUpdateRequested += (_, _) =>
            UpdateTimelineIndicators(timelineControlViewModel);

        UpdateTimelineIndicators(timelineControlViewModel);
    }

    private void UpdateTimelineIndicators(ITimelineControlViewModel timelineControlViewModel)
    {
        var timelineNavigationVm = timelineControlViewModel.TimelineNavigationViewModel;

        var timelineIndicators = _timelineIndicatorsFactory.GetTimelineIndicators(
            timelineNavigationVm.MovementPosition,
            timelineNavigationVm.ZoomLevel,
            timelineNavigationVm.TimelineControlWidth);

        var timelineSupporters = _timelineIndicatorsFactory.GetTimelineSupporters(
            timelineNavigationVm.MovementPosition,
            timelineNavigationVm.ZoomLevel,
            timelineNavigationVm.TimelineControlWidth);


        timelineControlViewModel.TimelineIndicators = new ObservableCollection<VideoPosition>(timelineIndicators);
        timelineControlViewModel.TimelineSupporters = new ObservableCollection<VideoPosition>(timelineSupporters);
    }
}