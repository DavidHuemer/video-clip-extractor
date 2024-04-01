using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel;

public interface IVideoPlayerControlPanelViewModel : IBaseViewModel
{
    VideoViewModel? Video { set; }
}