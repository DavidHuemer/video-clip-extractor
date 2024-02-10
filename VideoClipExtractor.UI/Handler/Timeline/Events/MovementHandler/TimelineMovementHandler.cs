using System.Windows;
using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Basics.MouseCursorHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.MovementHandler;

public class TimelineMovementHandler(
    IFrameworkElement timelineControl,
    TimelineControlViewModel viewModel,
    IMouseCursorHandler? mouseCursorHandler = null)
    : ITimelineMovementHandler
{
    public void StartMovement(Point position)
    {
        _lastMousePosition = position;
        _isMoving = true;
    }

    public void Move(Point position)
    {
        if (!_isMoving) return;

        var delta = position.X - _lastMousePosition.X;
        MoveByDelta(delta);

        // Check if the mouse cursor is outside of the control
        HandleMouseLeaveControl(position);
    }

    public void EndMovement() => _isMoving = false;

    private void MoveByDelta(double delta)
    {
        var tmpDelta = TimelineNavigation.MovementPosition - delta;
        if (tmpDelta < 0) tmpDelta = 0;

        TimelineNavigation.MovementPosition = tmpDelta;
    }

    private void HandleMouseLeaveControl(Point position)
    {
        if (IsMouseOutsideControl(position))
        {
            var newPosition = new Point
            {
                X = position.X,
                Y = position.Y,
            };

            if (newPosition.X < 0)
                newPosition.X = (int)timelineControl.ActualWidth + position.X;
            else if (newPosition.X > timelineControl.ActualWidth)
                newPosition.X = position.X - timelineControl.ActualWidth;

            if (newPosition.Y < 0)
                newPosition.Y = (int)timelineControl.ActualHeight;
            else if (newPosition.Y > timelineControl.ActualHeight)
                newPosition.Y = 0;

            var absolutePosition = timelineControl.PointToScreen(newPosition);
            _mouseCursorHandler.SetCursorPosition(absolutePosition);
            _lastMousePosition = newPosition;
        }
        else
        {
            _lastMousePosition = position;
        }
    }

    private bool IsMouseOutsideControl(Point position)
    {
        return position.X < 0 || position.X > timelineControl.ActualWidth ||
               position.Y < 0 || position.Y > timelineControl.ActualHeight;
    }

    #region Private Fields

    private Point _lastMousePosition;

    private readonly IMouseCursorHandler _mouseCursorHandler = mouseCursorHandler ?? new MouseCursorHandler();

    private bool _isMoving;

    private TimelineNavigationViewModel TimelineNavigation => viewModel.TimelineNavigationViewModel;

    #endregion
}