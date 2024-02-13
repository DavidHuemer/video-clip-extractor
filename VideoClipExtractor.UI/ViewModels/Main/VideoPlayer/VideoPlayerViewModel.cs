using BaseUI.Handler.ViewModelHandler;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

public class VideoPlayerViewModel : BaseViewModel, IVideoPlayerViewModel
{
    public VideoPlayerViewModel(IDependencyProvider provider)
    {
        Provider = provider;
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        VideoPlayerNavigationVm = viewModelProvider.GetViewModel<IVideoPlayerNavigationViewModel>();
        ExplorerViewModel = viewModelProvider.GetViewModel<IVideosExplorerViewModel>();
        ControlPanelViewModel = viewModelProvider.GetViewModel<IControlPanelViewModel>();

        var listener = new ViewModelPropertyListener(ExplorerViewModel);
        listener.AddPropertyListener(nameof(IVideosExplorerViewModel.SelectedVideo),
            (VideoViewModel? video) => { ControlPanelViewModel.Video = video; });
    }

    #region Properties

    public IVideosExplorerViewModel ExplorerViewModel { get; }

    #endregion

    #region Properties

    [DoNotNotify] public IVideoPlayerNavigationViewModel VideoPlayerNavigationVm { get; }

    public IDependencyProvider Provider { get; }
    [DoNotNotify] public IControlPanelViewModel ControlPanelViewModel { get; }

    #endregion
}