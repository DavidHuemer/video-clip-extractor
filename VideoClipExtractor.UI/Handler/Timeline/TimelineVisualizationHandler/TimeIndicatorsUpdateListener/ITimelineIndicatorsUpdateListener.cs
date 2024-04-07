using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

namespace VideoClipExtractor.UI.Handler.Timeline.TimelineVisualizationHandler.TimeIndicatorsUpdateListener;

public interface ITimelineIndicatorsUpdateListener
{
    event EventHandler TimelineIndicatorsUpdateRequested;

    void Setup(ITimelineControlViewModel timelineControlViewModel);
}