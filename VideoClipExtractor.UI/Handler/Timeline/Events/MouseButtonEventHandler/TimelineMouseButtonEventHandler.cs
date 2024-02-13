using System.Windows;
using System.Windows.Input;
using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Events;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using JetBrains.Annotations;
using VideoClipExtractor.Data.UI.Timeline;
using VideoClipExtractor.UI.Handler.Timeline.Events.MarkerEventHandler;
using VideoClipExtractor.UI.Handler.Timeline.Events.MovementEventHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.MouseButtonEventHandler;

[UsedImplicitly]
public class TimelineMouseButtonEventHandler(IDependencyProvider provider) : ITimelineMouseButtonEventHandler
{
    private const MouseButton MarkerMouseButton = MouseButton.Right;
    private const MouseButton MovementMouseButton = MouseButton.Middle;

    private readonly ITimelineMarkerEventHandler _timelineMarkerEventHandler =
        provider.GetDependency<ITimelineMarkerEventHandler>();

    private readonly ITimelineMovementEventHandler _timelineMovementEventHandler =
        provider.GetDependency<ITimelineMovementEventHandler>();

    private readonly ITimelineNavigationViewModel _timelineNavigationViewModel = provider
        .GetDependency<IViewModelProvider>()
        .GetViewModel<ITimelineNavigationViewModel>();

    private IFrameworkElement? _timelineControl;

    public void Setup(IFrameworkElement timelineControl)
    {
        _timelineControl = timelineControl;
        timelineControl.MouseDown += OnMouseDown;
        timelineControl.MouseUp += OnMouseUp;
    }

    private void OnMouseDown(object? sender, MouseButtonEventArgsWrapper e)
    {
        if (_timelineControl == null) return;

        if (_timelineNavigationViewModel.MovementState != MovementState.None)
        {
            Release();
            return;
        }

        _timelineControl.CaptureMouse();
        HandlePressedMouseButton(e.Button, e.GetPosition(_timelineControl));
    }

    private void HandlePressedMouseButton(MouseButton mouseButton, Point position)
    {
        switch (mouseButton)
        {
            case MarkerMouseButton:
                _timelineNavigationViewModel.MovementState = MovementState.MarkerMovement;
                _timelineMarkerEventHandler.StartMarkerMovement(position);
                break;
            case MovementMouseButton:
                _timelineNavigationViewModel.MovementState = MovementState.TimelineMovement;
                _timelineMovementEventHandler.StartMovement(position);
                break;
        }
    }

    private void OnMouseUp(object? sender, MouseButtonEventArgsWrapper e)
    {
        Release();
    }

    private void Release()
    {
        _timelineNavigationViewModel.MovementState = MovementState.None;
        _timelineControl?.ReleaseMouseCapture();
        _timelineMarkerEventHandler.StopMarkerMovement();
        _timelineMovementEventHandler.StopMovement();
    }
}