using System.Windows;
using BaseUI.Basics.FrameworkElementWrapper;
using VideoClipExtractor.Data.UI.Timeline;
using VideoClipExtractor.UI.Handler.Timeline.Events.MovementHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.NavigationEventHandler;

public class TimelineNavigationEventHandler(
    IFrameworkElement control,
    TimelineControlViewModel viewModel,
    ITimelineMovementHandler? movementHandler = null)
    : ITimelineNavigationEventHandler
{
    private readonly ITimelineMovementHandler _timelineMovementHandler =
        movementHandler ?? new TimelineMovementHandler(control, viewModel);

    private TimelineNavigationViewModel TimelineNavigation =>
        viewModel.TimelineNavigationViewModel;

    public void MarkerMouseButtonDown(Point position)
    {
        if (TimelineNavigation.MovementState == MovementState.TimelineMovement) return;

        if (TimelineNavigation.MovementState != MovementState.MarkerMovement)
            TimelineNavigation.MovementState = MovementState.MarkerMovement;

        UpdateMarkerPos(position);
    }

    public void MovementMouseButtonDown(Point position)
    {
        if (TimelineNavigation.MovementState == MovementState.MarkerMovement)
        {
            TimelineNavigation.MovementState = MovementState.None;
            return;
        }

        TimelineNavigation.MovementState = MovementState.TimelineMovement;
        _timelineMovementHandler.StartMovement(position);
    }

    public void MouseButtonUp()
    {
        TimelineNavigation.MovementState = MovementState.None;
    }

    public void MouseMove(Point position)
    {
        switch (TimelineNavigation.MovementState)
        {
            case MovementState.None:
                return;
            case MovementState.MarkerMovement:
                // TODO: Pause Video
                UpdateMarkerPos(position);
                break;
            default:
                _timelineMovementHandler.Move(position);
                break;
        }
    }

    private void UpdateMarkerPos(Point position)
    {
        var movementPos = position.X - 200 + TimelineNavigation.MovementPosition;
        if (movementPos < 0) movementPos = 0;
        viewModel.SetFrameByPosition(movementPos);
    }
}