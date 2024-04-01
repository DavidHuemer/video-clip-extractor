using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Data.UI.Video;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigationEditor;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerNavigation;

/// <summary>
///     Viewmodel for the navigation part of the video player.
///     Choosing next video, export, ...
/// </summary>
[Singleton]
public class VideoPlayerNavigationViewModel : BaseViewModelContainer, IVideoPlayerNavigationViewModel
{
    public VideoPlayerNavigationViewModel(IDependencyProvider provider) : base(provider)
    {
        VideoPlayerNavigationEditor = ViewModelProvider.Get<IVideoPlayerNavigationEditor>();
    }

    public VideoPosition? VideoLength { get; private set; }

    public int FrameCount => VideoLength?.Frame ?? 0;

    public IVideoPlayerNavigationEditor VideoPlayerNavigationEditor { get; set; }

    public VideoViewModel? Video
    {
        set
        {
            if (value == null)
            {
                VideoLength = null;
                return;
            }

            var endPos = new VideoPosition(value.VideoInfo.Duration, value.VideoInfo.FrameRate);
            VideoLength = endPos;
        }
    }
}