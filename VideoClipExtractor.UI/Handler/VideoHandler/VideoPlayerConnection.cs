using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;
using VideoClipExtractor.Data.Events;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.Handler.VideoHandler;

/// <summary>
/// This class is responsible for connecting the video player to the video player view model
/// </summary>
public class VideoPlayerConnection
{
    private readonly IVideoNavigationViewModel _videoNavigationViewModel;
    private readonly IVideoPlayer _videoPlayer;

    public VideoPlayerConnection(IVideoPlayer videoPlayer, IVideoPlayerViewModel videoPlayerViewModel)
    {
        _videoPlayer = videoPlayer;
        var dependencyProvider = videoPlayerViewModel.Provider;

        var viewModelProvider = dependencyProvider.GetDependency<IViewModelProvider>();
        _videoNavigationViewModel = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();


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

    private void OnPositionChangeRequested(object? sender, VideoPositionEventArgs e)
    {
        _videoPlayer.Position = e.VideoPosition.Duration.TimeSpan;
        _videoNavigationViewModel.VideoPosition = e.VideoPosition;
    }
}