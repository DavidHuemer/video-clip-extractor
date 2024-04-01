using BaseUI.ViewModels;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

public interface IVideoPlayerViewModel : IBaseViewModelContainer
{
    public VideoViewModel? Video { set; }
}