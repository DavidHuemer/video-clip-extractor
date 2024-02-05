using BaseUI.Services.DependencyInjection;
using BaseUI.Services.ViewModelProvider;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

public class VideoPlayerViewModel : BaseViewModel
{
    public VideoPlayerViewModel(IDependencyProvider provider)
    {
        VideoPlayerNavigationVm = new VideoPlayerNavigationViewModel(provider);
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();

        ExplorerViewModel = viewModelProvider.GetViewModel<IVideosExplorerViewModel>();

        //provider.GetDependency<IVideoManager>().VideoChanged += OnVideoChanged;
    }

    #region Properties

    [DoNotNotify] public VideoPlayerNavigationViewModel VideoPlayerNavigationVm { get; }

    #endregion

    private void OnVideoChanged(object? sender, VideoChangedEventArgs e)
    {
        Video = e.Video;
    }

    #region Properties

    public IVideosExplorerViewModel ExplorerViewModel { get; set; }

    public Video? Video { get; private set; }

    #endregion
}