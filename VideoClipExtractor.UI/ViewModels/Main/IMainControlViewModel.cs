using BaseUI.ViewModels;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.ViewModels.Main;

public interface IMainControlViewModel : IBaseViewModel
{
    IVideoPlayerViewModel VideoPlayerVm { get; }
}