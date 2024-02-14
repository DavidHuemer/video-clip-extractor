using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControlPanel;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline;

/// <summary>
/// The ViewModel for the entire timeline.
/// </summary>
public interface ITimelineViewModel
{
    public ITimelineControlPanelViewModel TimelineControlPanelViewModel { get; set; }

    public ITimelineControlViewModel TimelineControlViewModel { get; set; }

    public VideoViewModel? Video { set; }
}