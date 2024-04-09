using System.Windows;
using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Basics.MouseCursorHandler;
using BaseUI.Events;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.MovementEventHandler;

[Singleton]
public class TimelineMovementEventHandler : ITimelineMovementEventHandler
{
    private readonly IMouseCursorHandler _mouseCursorHandler;

    private readonly ITimelineNavigationViewModel _timelineNavigationViewModel;
    private bool _isMoving;
    private Point _lastMousePosition;

    private IFrameworkElement? _timelineControl;

    public TimelineMovementEventHandler(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _timelineNavigationViewModel = viewModelProvider.Get<ITimelineNavigationViewModel>();
        _mouseCursorHandler = provider.GetDependency<IMouseCursorHandler>();
    }

    public void StartMovement(Point position)
    {
        _lastMousePosition = position;
        _isMoving = true;
    }

    public void Setup(IFrameworkElement timelineControl)
    {
        _timelineControl = timelineControl;
        timelineControl.MouseMove += OnTimelineMouseMove;
    }

    public void StopMovement() => _isMoving = false;

    private void OnTimelineMouseMove(object? sender, MouseEventArgsWrapper e)
    {
        if (!_isMoving || _timelineControl == null) return;

        var position = e.GetPosition(_timelineControl);
        Move(position, _timelineControl);
    }

    private void Move(Point position, IFrameworkElement timelineControl)
    {
        var delta = position.X - _lastMousePosition.X;
        MoveByDelta(delta);

        // Check if the mouse cursor is outside of the control
        HandleMouseLeaveControl(position, timelineControl);
    }

    private void MoveByDelta(double delta)
    {
        var tmpDelta = _timelineNavigationViewModel.MovementPosition - delta;
        if (tmpDelta < 0) tmpDelta = 0;

        _timelineNavigationViewModel.MovementPosition = tmpDelta;
    }

    private void HandleMouseLeaveControl(Point position, IFrameworkElement timelineControl)
    {
        if (IsMouseOutsideControl(position, timelineControl))
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

    private bool IsMouseOutsideControl(Point position, IFrameworkElement timelineControl)
    {
        return position.X < 0 || position.X > timelineControl.ActualWidth ||
               position.Y < 0 || position.Y > timelineControl.ActualHeight;
    }
}