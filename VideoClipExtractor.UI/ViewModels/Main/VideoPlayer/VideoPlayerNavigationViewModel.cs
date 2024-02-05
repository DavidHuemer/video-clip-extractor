using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

/// <summary>
///     Viewmodel for the navigation part of the video player.
///     Choosing next video, export, ...
/// </summary>
public class VideoPlayerNavigationViewModel : BaseViewModel
{
    public VideoPlayerNavigationViewModel(IDependencyProvider provider)
    {
    }
}