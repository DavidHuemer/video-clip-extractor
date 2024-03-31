using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

[Singleton]
public class VideoPlayerViewModel : BaseViewModelContainer, IVideoPlayerViewModel
{
    public VideoPlayerViewModel(IDependencyProvider provider) : base(provider)
    {
        VideoPlayerNavigationVm = ViewModelProvider.Get<IVideoPlayerNavigationViewModel>();
        ExplorerViewModel = ViewModelProvider.Get<IVideosExplorerViewModel>();
        VideoNavigationViewModel = ViewModelProvider.Get<IVideoNavigationViewModel>();
    }

    #region Properties

    public IVideosExplorerViewModel ExplorerViewModel { get; }

    #endregion

    #region Properties

    [DoNotNotify] public IVideoPlayerNavigationViewModel VideoPlayerNavigationVm { get; }

    [DoNotNotify] public IVideoNavigationViewModel VideoNavigationViewModel { get; }

    #endregion
}