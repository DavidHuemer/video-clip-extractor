using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;

/// <summary>
/// View model for the actual timeline control
/// </summary>
public interface ITimelineControlViewModel
{
    public TimelineNavigationViewModel TimelineNavigationViewModel { get; set; }
}