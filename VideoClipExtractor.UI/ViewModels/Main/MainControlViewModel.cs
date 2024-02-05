using BaseUI.Services.DependencyInjection;
using BaseUI.Services.ViewModelProvider;
using BaseUI.ViewModels;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.ViewModels.Main;

/// <summary>
///     View model for the main control.
/// </summary>
public class MainControlViewModel : BaseViewModel
{
    public MainControlViewModel(IDependencyProvider provider)
    {
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        ExplorerVm = viewModelProvider.GetViewModel<IVideosExplorerViewModel>();
        VideoPlayerVm = new VideoPlayerViewModel(provider);
    }

    public IVideosExplorerViewModel ExplorerVm { get; }

    public VideoPlayerViewModel VideoPlayerVm { get; }
}