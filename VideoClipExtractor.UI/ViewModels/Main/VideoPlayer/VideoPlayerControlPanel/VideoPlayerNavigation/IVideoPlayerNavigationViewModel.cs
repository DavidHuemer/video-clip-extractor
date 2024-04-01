using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigationEditor;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigation;

public interface IVideoPlayerNavigationViewModel : IBaseViewModel
{
    VideoViewModel? Video { set; }

    IVideoPlayerNavigationEditor VideoPlayerNavigationEditor { get; }
}