using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Data;
using BaseUI.Events;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Data.UI.Timeline;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.ZoomEventHandler;

public class TimelineZoomEventHandler : ITimelineZoomEventHandler
{
    private readonly ITimelineNavigationViewModel _timelineNavigation;
    private readonly ITimelinePositionHandler _timelinePositionHandler;
    private IFrameworkElement? _timelineControl;

    public TimelineZoomEventHandler(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _timelineNavigation = viewModelProvider.Get<ITimelineNavigationViewModel>();
        _timelinePositionHandler = provider.GetDependency<ITimelinePositionHandler>();
    }

    public void Setup(IFrameworkElement timelineControl)
    {
        _timelineControl = timelineControl;
        timelineControl.MouseWheel += OnTimelineMouseWheel;
    }

    public void Zoom(ZoomDirection zoomDirection)
    {
        if (_timelineControl == null) return;

        var tmpZoomLevel = _timelineNavigation.ZoomLevel + (int)zoomDirection;
        if (tmpZoomLevel < 1) return;

        var centerPos =
            _timelinePositionHandler.GetCenterPosition(_timelineNavigation.MovementPosition,
                _timelineControl.ActualWidth);
        var centerFrame =
            _timelinePositionHandler.GetFrameAtPosition(centerPos, _timelineNavigation.ZoomLevel);

        _timelineNavigation.ZoomLevel = tmpZoomLevel;

        var newCenterPos =
            _timelinePositionHandler.GetPositionAtFrame(centerFrame, _timelineNavigation.ZoomLevel);
        var deltaMovement = newCenterPos - centerPos;
        var tmpMovementPos = _timelineNavigation.MovementPosition + deltaMovement;
        if (tmpMovementPos < 0) tmpMovementPos = 0;
        _timelineNavigation.MovementPosition = tmpMovementPos;
    }

    private void OnTimelineMouseWheel(object? sender, MouseWheelEventArgsWrapper e)
    {
        if (_timelineNavigation.MovementState == MovementState.None)
            Zoom(e.Delta > 0 ? ZoomDirection.In : ZoomDirection.Out);
    }
}