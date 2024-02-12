using System.ComponentModel;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.UI.Controls.VideoPlayer;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.ActionBar.VideoNavigation;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel.Timeline.TimelineControl.TimelineNavigation;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.Handler.VideoHandler.PositionInterrogator;

/// <summary>
/// Responsible for interrogating the video player for its current position.
///
/// Handles as a mediator between the video player and the video player view model.
/// </summary>
public class VideoPositionInterrogator
{
    private readonly IVideoPositionDispatcher _dispatcher;
    private readonly IVideoPlayer _videoPlayer;
    private readonly IVideoPlayerViewModel _videoPlayerViewModel;
    private bool _mediaOpened;

    public VideoPositionInterrogator(IVideoPlayer videoPlayer, IVideoPlayerViewModel videoPlayerViewModel,
        IVideoPositionDispatcher? videoPositionDispatcher = null)
    {
        _videoPlayer = videoPlayer;
        _videoPlayerViewModel = videoPlayerViewModel;
        videoPlayer.VideoOpened += OnVideoPlayerOnVideoOpened;
        _dispatcher = videoPositionDispatcher ?? new VideoPositionDispatcher();
        _dispatcher.PositionDispatched += (_, _) => OnVideoPositionDispatched();

        videoPlayerViewModel.PropertyChanged += OnVideoPlayerViewModelOnPropertyChanged;
    }

    private void OnVideoPlayerViewModelOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
    {
        if (e.PropertyName == nameof(VideoNavigationViewModel.PlayStatus))
            OnPlayStatusChanged(VideoNavigationViewModel.PlayStatus);
    }

    private void OnVideoPositionDispatched()
    {
        TimelineNavigationViewModel.VideoPosition = new VideoPosition(_videoPlayer.Position);
    }

    private void OnPlayStatusChanged(PlayStatus playStatus)
    {
        if (!_mediaOpened) return;

        if (playStatus == PlayStatus.Playing)
            _dispatcher.Start();
        else
            _dispatcher.Stop();
    }

    private void OnVideoPlayerOnVideoOpened(object? sender, EventArgs e)
    {
        _mediaOpened = true;

        if (VideoNavigationViewModel.PlayStatus == PlayStatus.Playing)
            _dispatcher.Start();
    }

    #region Shortcuts

    private IVideoNavigationViewModel VideoNavigationViewModel =>
        _videoPlayerViewModel.ControlPanelViewModel.ActionBarViewModel.VideoNavigationViewModel;

    private TimelineNavigationViewModel TimelineNavigationViewModel =>
        _videoPlayerViewModel.ControlPanelViewModel.TimelineViewModel.TimelineControlViewModel
            .TimelineNavigationViewModel;

    #endregion
}