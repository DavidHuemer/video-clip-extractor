using BaseUI.Services.DependencyInjection;
using BaseUI.Services.ViewModelProvider;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

public class VideoPlayerViewModel : BaseViewModel
{
    public VideoPlayerViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        VideoPlayerNavigationVm = viewModelProvider.GetViewModel<IVideoPlayerNavigationViewModel>();
        ExplorerViewModel = viewModelProvider.GetViewModel<IVideosExplorerViewModel>();
    }

    #region Properties

    [DoNotNotify] public IVideoPlayerNavigationViewModel VideoPlayerNavigationVm { get; }

    #endregion

    #region Properties

    public IVideosExplorerViewModel ExplorerViewModel { get; }

    #endregion
}