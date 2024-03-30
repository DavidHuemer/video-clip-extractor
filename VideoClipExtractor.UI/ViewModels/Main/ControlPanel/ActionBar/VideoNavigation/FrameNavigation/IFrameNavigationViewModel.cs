using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

public interface IFrameNavigationViewModel
{
    VideoViewModel? Video { get; set; }

    VideoPosition VideoPosition { get; set; }
}