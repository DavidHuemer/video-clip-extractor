using System.Windows;
using VideoClipExtractor.UI.Handler.VideoHandler;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.Panels.VideoPlayerPanels;

public partial class VideoPlayerPanel
{
    public VideoPlayerPanel()
    {
        InitializeComponent();
    }

    private IVideoPlayerViewModel? VideoPlayerViewModel => DataContext as IVideoPlayerViewModel;

    private void VideoPlayerPanel_OnDataContextChanged(object sender, DependencyPropertyChangedEventArgs e)
    {
        if (VideoPlayerViewModel == null) return;

        _ = new VideoPlayerConnection(VideoPlayer, VideoPlayerViewModel);
    }
}