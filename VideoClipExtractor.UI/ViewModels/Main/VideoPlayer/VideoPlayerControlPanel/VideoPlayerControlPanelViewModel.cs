using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerActionBar;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigation;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel;

[Transient]
public class VideoPlayerControlPanelViewModel : BaseViewModelContainer, IVideoPlayerControlPanelViewModel
{
    public VideoPlayerControlPanelViewModel(IDependencyProvider provider) : base(provider)
    {
        VideoPlayerNavigationViewModel = ViewModelProvider.Get<IVideoPlayerNavigationViewModel>();
        ActionBarViewModel = ViewModelProvider.Get<IVideoPlayerActionBarViewModel>();
    }

    [DoNotNotify] public IVideoPlayerNavigationViewModel VideoPlayerNavigationViewModel { get; }

    [DoNotNotify] public IVideoPlayerActionBarViewModel ActionBarViewModel { get; }

    [DoNotNotify]
    public VideoViewModel? Video
    {
        set => VideoPlayerNavigationViewModel.Video = value;
    }
}