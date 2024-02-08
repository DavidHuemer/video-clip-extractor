using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar;

public interface IActionBarViewModel
{
    public IVideoNavigationViewModel VideoNavigationViewModel { get; set; }
    VideoViewModel? Video { set; }
}