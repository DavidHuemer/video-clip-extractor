using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation.FrameNavigation;

namespace VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;

/// <summary>
/// Responsible for interrogating the video player for its current position.
///
/// Handles as a mediator between the video player and the video player view model.
/// </summary>
[Transient]
public class VideoPositionInterrogator : IVideoPositionInterrogator
{
    private readonly IVideoPositionDispatcher _dispatcher;

    private readonly IFrameNavigationViewModel _frameNavigationViewModel;
    private readonly IVideoManager _videoManager;

    private readonly IVideoNavigationViewModel _videoNavigationViewModel;

    private VideoPosition1 _lastPosition1 = new(0);
    private IVideoPlayer? _videoPlayer;

    public VideoPositionInterrogator(IDependencyProvider provider)
    {
        _dispatcher = provider.GetDependency<IVideoPositionDispatcher>();
        _dispatcher.PositionDispatched += (_, _) => OnVideoPositionDispatched();

        _videoManager = provider.GetDependency<IVideoManager>();


        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _videoNavigationViewModel = viewModelProvider.Get<IVideoNavigationViewModel>();

        _frameNavigationViewModel = viewModelProvider.Get<IFrameNavigationViewModel>();

        _videoNavigationViewModel.PropertyChanged += (_, e) =>
        {
            if (e.PropertyName == nameof(IVideoNavigationViewModel.PlayStatus))
                OnPlayStatusChanged(_videoNavigationViewModel.PlayStatus);
        };
    }

    public void Setup(IVideoPlayer videoPlayer) => _videoPlayer = videoPlayer;

    private void OnVideoPositionDispatched()
    {
        if (_videoPlayer == null) return;

        var video = _videoManager.Video;
        if (video == null) return;

        var frameRate = video.VideoInfo.FrameRate;

        var x = new VideoPosition(_videoPlayer.Position, frameRate);
        _frameNavigationViewModel.VideoPosition = x;

        _lastPosition1 = new VideoPosition1(_videoPlayer.Position);
        _frameNavigationViewModel.VideoPosition1 = _lastPosition1;
    }

    private void OnPlayStatusChanged(PlayStatus playStatus)
    {
        if (playStatus == PlayStatus.Playing)
            _dispatcher.Start();
        else
            _dispatcher.Stop();
    }
}