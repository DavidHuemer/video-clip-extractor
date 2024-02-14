using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.TimelineExtraction;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar;

public interface IActionBarViewModel
{
    public IVideoNavigationViewModel VideoNavigationViewModel { get; set; }

    ITimelineExtractionBarViewModel TimelineExtractionBarViewModel { get; }

    VideoViewModel? Video { set; }
}