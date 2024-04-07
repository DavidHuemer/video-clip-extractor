using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicatorsUpdateListener;

[Transient]
public class TimelineIndicatorsUpdateListener : ITimelineIndicatorsUpdateListener
{
    private readonly string[] _allowedProperties =
    [
        nameof(TimelineControlViewModel.TimelineNavigationViewModel.ZoomLevel),
        nameof(TimelineControlViewModel.TimelineNavigationViewModel.MovementPosition),
        nameof(TimelineControlViewModel.TimelineNavigationViewModel.TimelineControlWidth),
    ];

    public event EventHandler? TimelineIndicatorsUpdateRequested;

    public void Setup(ITimelineControlViewModel timelineControlViewModel)
    {
        timelineControlViewModel.PropertyChanged += (_, e) =>
        {
            if (_allowedProperties.Contains(e.PropertyName))
                TimelineIndicatorsUpdateRequested?.Invoke(this, EventArgs.Empty);
        };
    }
}