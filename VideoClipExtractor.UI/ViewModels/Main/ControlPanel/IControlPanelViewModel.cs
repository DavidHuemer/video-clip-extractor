using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel;

public interface IControlPanelViewModel
{
    public IActionBarViewModel ActionBarViewModel { get; set; }
    VideoViewModel? Video { set; }

    public ITimelineViewModel TimelineViewModel { get; set; }
}