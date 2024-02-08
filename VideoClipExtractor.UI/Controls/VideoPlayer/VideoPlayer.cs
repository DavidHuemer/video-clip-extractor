using System.Windows;
using System.Windows.Controls;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.UI.Controls.VideoPlayer;

/// <summary>
///     Responsible for playing a video
/// </summary>
public class VideoPlayer : MediaElement
{
    #region PlayStatus

    public static readonly DependencyProperty PlayStatusProperty =
        DependencyProperty.Register(nameof(PlayStatus), typeof(PlayStatus), typeof(VideoPlayer),
            new PropertyMetadata(PlayStatus.Paused, OnPlayStatusChanged));

    public PlayStatus PlayStatus
    {
        get => (PlayStatus)GetValue(PlayStatusProperty);
        set => SetValue(PlayStatusProperty, value);
    }

    public VideoPlayer()
    {
        LoadedBehavior = MediaState.Manual;
    }

    private static void OnPlayStatusChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
    {
        var videoPlayer = (VideoPlayer)d;

        if ((PlayStatus)e.NewValue == PlayStatus.Playing)
        {
            videoPlayer.Play();
        }
        else
        {
            videoPlayer.Pause();
        }
    }

    #endregion
}