using System.Windows.Input;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

public interface IVideoNavigationViewModel
{
    public PlayStatus PlayStatus { get; set; }
    VideoViewModel? Video { get; set; }

    ICommand PlayPause { get; }
}