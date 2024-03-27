using BaseUI.ViewModels;

namespace VideoClipExtractor.UI.ViewModels.SetupViewModels.VideosSetupViewModels;

public interface IVideosSetupViewModel : IBaseViewModel
{
    event EventHandler Finish;
}