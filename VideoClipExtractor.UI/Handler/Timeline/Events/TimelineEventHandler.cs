using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Data;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events;

public class TimelineEventHandler : ITimelineEventHandler
{
    public TimelineEventHandler(IFrameworkElement timelineControl, TimelineControlViewModel viewModel,
        ITimelinePositionHandler? timelinePositionHandler = null)
    {
        _viewModel = viewModel;
        _timelineControl = timelineControl;
        _timelinePositionHandler = timelinePositionHandler ?? new TimelinePositionHandler();
    }

    public void Zoom(ZoomDirection zoomDirection)
    {
        var tmpZoomLevel = TimelineNavigation.ZoomLevel + (int)zoomDirection;
        if (tmpZoomLevel < 1) return;

        var centerPos =
            _timelinePositionHandler.GetCenterPosition(TimelineNavigation.MovementPosition,
                _timelineControl.ActualWidth);
        var centerFrame =
            _timelinePositionHandler.GetFrameAtPosition(centerPos, TimelineNavigation.ZoomLevel);
        TimelineNavigation.ZoomLevel = tmpZoomLevel;

        var newCenterPos =
            _timelinePositionHandler.GetPositionAtFrame(centerFrame, TimelineNavigation.ZoomLevel);
        var deltaMovement = newCenterPos - centerPos;
        var tmpMovementPos = TimelineNavigation.MovementPosition + deltaMovement;
        if (tmpMovementPos < 0) tmpMovementPos = 0;
        TimelineNavigation.MovementPosition = tmpMovementPos;
    }

    #region Private Fields

    /// <summary>
    /// The UI element of the timeline control
    /// </summary>
    private readonly IFrameworkElement _timelineControl;

    /// <summary>
    /// The <see cref="TimelineControlViewModel"/> that should be informed about the events
    /// </summary>
    private readonly TimelineControlViewModel _viewModel;

    private TimelineNavigationViewModel TimelineNavigation =>
        _viewModel.TimelineNavigationViewModel;

    private readonly ITimelinePositionHandler _timelinePositionHandler;

    #endregion
}