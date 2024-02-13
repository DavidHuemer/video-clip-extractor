using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.UI.Handler.Timeline.Events.MarkerEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.MouseButtonEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.MovementEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.ZoomEventHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events;

/// <summary>
/// Handles events of the timeline control
/// </summary>
public class TimelineEventHandler
{
    private readonly IFrameworkElement _timelineControl;
    private readonly ITimelineNavigationViewModel _timelineNavigationViewModel;

    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="timelineControl">The UI element of the timeline control</param>
    /// <param name="viewModel">The <see cref="TimelineControlViewModel"/> that should be informed about the events</param>
    public TimelineEventHandler(IFrameworkElement timelineControl, ITimelineControlViewModel viewModel)
    {
        _timelineControl = timelineControl;
        var dependencyProvider = viewModel.Provider;

        var timelineZoomEventHandler = dependencyProvider.GetDependency<ITimelineZoomEventHandler>();
        timelineZoomEventHandler.Setup(timelineControl);

        var mouseButtonEventHandler = dependencyProvider.GetDependency<ITimelineMouseButtonEventHandler>();
        mouseButtonEventHandler.Setup(timelineControl);

        var markerEventHandler = dependencyProvider.GetDependency<ITimelineMarkerEventHandler>();
        markerEventHandler.Setup(timelineControl);

        var movementEventHandler = dependencyProvider.GetDependency<ITimelineMovementEventHandler>();
        movementEventHandler.Setup(timelineControl);

        var viewModelProvider = dependencyProvider.GetDependency<IViewModelProvider>();
        _timelineNavigationViewModel = viewModelProvider.GetViewModel<ITimelineNavigationViewModel>();

        _timelineNavigationViewModel.TimelineControlWidth = timelineControl.ActualWidth;

        timelineControl.SizeChanged += TimelineControlOnSizeChanged;
    }

    private void TimelineControlOnSizeChanged(object? sender, EventArgs e)
    {
        _timelineNavigationViewModel.TimelineControlWidth = _timelineControl.ActualWidth;
    }
}