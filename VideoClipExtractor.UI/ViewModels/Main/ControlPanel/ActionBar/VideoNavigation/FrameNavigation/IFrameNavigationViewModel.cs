using System.Windows.Input;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

public interface IFrameNavigationViewModel : IBaseViewModel
{
    VideoViewModel? Video { get; set; }

    VideoPosition1 VideoPosition1 { get; set; }

    VideoPosition VideoPosition { get; set; }

    ICommand GoBackward { get; }

    ICommand GoForward { get; }
}