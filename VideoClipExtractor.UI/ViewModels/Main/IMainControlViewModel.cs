using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.ViewModels.Main;

public interface IMainControlViewModel
{
    IVideoPlayerViewModel VideoPlayerVm { get; }
}