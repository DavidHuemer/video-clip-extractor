using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.ViewModels.Main;

/// <summary>
/// View model for the main control.
/// </summary>
public class MainControlViewModel(IDependencyProvider provider) : BaseViewModel
{
    public VideosExplorerViewModel ExplorerVm { get; set; } = new(provider);

    public VideoPlayerViewModel VideoPlayerVm { get; set; } = new(provider);
}