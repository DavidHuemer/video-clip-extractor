using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Data;
using BaseUI.Events;
using VideoClipExtractor.UI.Handler.Timeline.Events.NavigationEventHandler;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.UI.Handler.Timeline.Events;

/// <summary>
/// Catches events from the timeline control
/// </summary>
public class TimelineEventCatcher
{
    /// <summary>
    /// Constructor
    /// </summary>
    /// <param name="timelineControl">The UI element of the timeline control</param>
    /// <param name="viewModel">The <see cref="TimelineControlViewModel"/> that should be informed about the events</param>
    /// <param name="timelineEventHandler"></param>
    /// <param name="mouseButtonEventHandler"></param>
    public TimelineEventCatcher(IFrameworkElement timelineControl, TimelineControlViewModel viewModel,
        ITimelineZoomEventHandler? timelineEventHandler = null,
        ITimelineNavigationEventHandler? mouseButtonEventHandler = null)
    {
        _timelineControl = timelineControl;
        _viewModel = viewModel;

        _timelineZoomEventHandler =
            timelineEventHandler ?? new TimelineZoomEventHandler(_timelineControl, viewModel);
        _timelineNavigationHandler =
            mouseButtonEventHandler ?? new TimelineNavigationEventHandler(_timelineControl, viewModel);

        _viewModel.TimelineNavigationViewModel.TimelineControlWidth = _timelineControl.ActualWidth;

        SetupEvents();
    }

    private void SetupEvents()
    {
        _timelineControl.SizeChanged += OnTimelineSizeChanged;
        _timelineControl.MouseDown += OnTimelineMouseDown;
        _timelineControl.MouseUp += OnTimelineMouseUp;
        _timelineControl.MouseMove += OnTimelineMouseMove;

        _timelineControl.MouseWheel += OnTimelineMouseWheel;
    }

    private void OnTimelineSizeChanged(object? sender, EventArgs e) =>
        _viewModel.TimelineNavigationViewModel.TimelineControlWidth = _timelineControl.ActualWidth;

    private void OnTimelineMouseWheel(object? sender, MouseWheelEventArgsWrapper e) =>
        _timelineZoomEventHandler.Zoom(e.Delta > 0 ? ZoomDirection.In : ZoomDirection.Out);

    private void OnTimelineMouseDown(object? sender, MouseButtonEventArgsWrapper e)
    {
        _timelineControl.CaptureMouse();

        switch (e.Button)
        {
            case TimelineZoomEventHandler.MarkerMouseButton:
                _timelineNavigationHandler.MarkerMouseButtonDown(e.GetPosition(_timelineControl));
                break;
            case TimelineZoomEventHandler.MovementMouseButton:
                _timelineNavigationHandler.MovementMouseButtonDown(e.GetPosition(_timelineControl));
                break;
        }
    }

    private void OnTimelineMouseUp(object? sender, MouseButtonEventArgsWrapper e)
    {
        _timelineControl.ReleaseMouseCapture();
        _timelineNavigationHandler.MouseButtonUp();
    }

    private void OnTimelineMouseMove(object? sender, MouseEventArgsWrapper e)
    {
        _timelineNavigationHandler.MouseMove(e.GetPosition(_timelineControl));
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

    /// <summary>
    /// Handles the zoom events of the timeline control
    /// </summary>
    private readonly ITimelineZoomEventHandler _timelineZoomEventHandler;

    private readonly ITimelineNavigationEventHandler _timelineNavigationHandler;

    #endregion
}