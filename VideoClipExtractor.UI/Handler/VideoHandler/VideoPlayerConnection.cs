using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionChangeRequestHandler;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.Handler.VideoHandler;

/// <summary>
/// This class is responsible for connecting the video player to the video player view model
/// </summary>
public class VideoPlayerConnection
{
    private readonly IFrameNavigationViewModel _frameNavigationViewModel;
    private readonly IVideoNavigationViewModel _videoNavigationViewModel;
    private readonly IVideoPlayer _videoPlayer;

    public VideoPlayerConnection(IVideoPlayer videoPlayer, IVideoPlayerViewModel videoPlayerViewModel)
    {
        _videoPlayer = videoPlayer;
        var dependencyProvider = videoPlayerViewModel.DependencyProvider;

        var viewModelProvider = dependencyProvider.GetDependency<IViewModelProvider>();
        _videoNavigationViewModel = viewModelProvider.Get<IVideoNavigationViewModel>();
        _frameNavigationViewModel = viewModelProvider.Get<IFrameNavigationViewModel>();


        SetupVideoPositionInterrogator(dependencyProvider);
        SetupPositionChange(dependencyProvider);
    }

    private void SetupVideoPositionInterrogator(IDependencyProvider dependencyProvider)
    {
        var videoPositionInterrogator = dependencyProvider.GetDependency<IVideoPositionInterrogator>();
        videoPositionInterrogator.Setup(_videoPlayer);
    }

    private void SetupPositionChange(IDependencyProvider dependencyProvider)
    {
        var videoChangeRequestHandler = dependencyProvider.GetDependency<IPositionChangeRequestHandler>();
        videoChangeRequestHandler.Setup(_videoPlayer);
    }

    // private void OnPositionChangeRequested(VideoPosition position)
    // {
    //     _videoPlayer.Position = position.Time;
    //     _frameNavigationViewModel.VideoPosition = position;
    //
    //     _videoNavigationViewModel.PlayStatus = PlayStatus.Playing;
    //     _videoNavigationViewModel.PlayStatus = PlayStatus.Paused;
    // }
}