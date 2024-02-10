using BaseUI.Basics.FrameworkElementWrapper;
using BaseUI.Data;
using BaseUI.Events;
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
    public TimelineEventCatcher(IFrameworkElement timelineControl, TimelineControlViewModel viewModel,
        ITimelineEventHandler? timelineEventHandler = null)
    {
        _timelineControl = timelineControl;
        _viewModel = viewModel;

        _timelineEventHandler = timelineEventHandler ?? new TimelineEventHandler(_timelineControl, viewModel);
        _viewModel.TimelineNavigationViewModel.TimelineControlWidth = _timelineControl.ActualWidth;

        SetupEvents();
    }

    private void SetupEvents()
    {
        _timelineControl.SizeChanged += OnTimelineSizeChanged;
        _timelineControl.MouseWheel += OnTimelineMouseWheel;
    }

    private void OnTimelineSizeChanged(object? sender, EventArgs e) =>
        _viewModel.TimelineNavigationViewModel.TimelineControlWidth = _timelineControl.ActualWidth;

    private void OnTimelineMouseWheel(object? sender, MouseWheelEventArgsWrapper e) =>
        _timelineEventHandler.Zoom(e.Delta > 0 ? ZoomDirection.In : ZoomDirection.Out);

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
    /// Handles the events of the timeline control
    /// </summary>
    private readonly ITimelineEventHandler _timelineEventHandler;

    #endregion
}