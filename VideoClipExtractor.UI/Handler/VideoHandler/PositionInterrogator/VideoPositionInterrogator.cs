using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;

namespace VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;

/// <summary>
/// Responsible for interrogating the video player for its current position.
///
/// Handles as a mediator between the video player and the video player view model.
/// </summary>
public class VideoPositionInterrogator : IVideoPositionInterrogator
{
    private readonly IVideoPositionDispatcher _dispatcher;

    private readonly IVideoNavigationViewModel _videoNavigationViewModel;

    private VideoPosition _lastPosition = new(0);
    private IVideoPlayer? _videoPlayer;

    public VideoPositionInterrogator(IDependencyProvider provider)
    {
        _dispatcher = provider.GetDependency<IVideoPositionDispatcher>();
        _dispatcher.PositionDispatched += (_, _) => OnVideoPositionDispatched();

        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        _videoNavigationViewModel = viewModelProvider.GetViewModel<IVideoNavigationViewModel>();

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

        _lastPosition = new VideoPosition(_videoPlayer.Position);
        _videoNavigationViewModel.VideoPosition = _lastPosition;
    }

    private void OnPlayStatusChanged(PlayStatus playStatus)
    {
        if (playStatus == PlayStatus.Playing)
            _dispatcher.Start();
        else
            _dispatcher.Stop();
    }
}