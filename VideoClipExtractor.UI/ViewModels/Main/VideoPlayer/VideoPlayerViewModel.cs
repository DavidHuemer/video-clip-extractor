using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

[Singleton]
public class VideoPlayerViewModel : BaseViewModelContainer, IVideoPlayerViewModel
{
    public VideoPlayerViewModel(IDependencyProvider provider) : base(provider)
    {
        ControlPanelViewModel = ViewModelProvider.Get<IVideoPlayerControlPanelViewModel>();

        VideoPlayerNavigationVm = ViewModelProvider.Get<IVideoPlayerNavigationViewModel>();
        ExplorerViewModel = ViewModelProvider.Get<IVideosExplorerViewModel>();
        VideoNavigationViewModel = ViewModelProvider.Get<IVideoNavigationViewModel>();
    }

    #region Properties

    [DoNotNotify] public IVideosExplorerViewModel ExplorerViewModel { get; }

    [DoNotNotify] public IVideoPlayerControlPanelViewModel ControlPanelViewModel { get; }

    [DoNotNotify] public IVideoPlayerNavigationViewModel VideoPlayerNavigationVm { get; }

    [DoNotNotify] public IVideoNavigationViewModel VideoNavigationViewModel { get; }

    [DoNotNotify]
    public VideoViewModel? Video
    {
        set => ControlPanelViewModel.Video = value;
    }

    #endregion
}