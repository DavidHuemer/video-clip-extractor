using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.Handler.VideoHandler;

/// <summary>
/// This class is responsible for connecting the video player to the video player view model
/// </summary>
public class VideoPlayerConnection
{
    private readonly IFrameNavigationViewModel _frameNavigationViewModel;
    private readonly IVideoPlayer _videoPlayer;

    public VideoPlayerConnection(IVideoPlayer videoPlayer, IVideoPlayerViewModel videoPlayerViewModel)
    {
        _videoPlayer = videoPlayer;
        var dependencyProvider = videoPlayerViewModel.DependencyProvider;

        var viewModelProvider = dependencyProvider.GetDependency<IViewModelProvider>();
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
        var videoPositionService = dependencyProvider.GetDependency<IVideoPositionService>();
        videoPositionService.PositionChangeRequested += OnPositionChangeRequested;
    }

    private void OnPositionChangeRequested(VideoPosition position)
    {
        _videoPlayer.Position = position.Duration.TimeSpan;
        _frameNavigationViewModel.VideoPosition = position;
    }
}