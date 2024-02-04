using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

public class VideoPlayerViewModel : BaseViewModel
{
    public VideoPlayerViewModel(IDependencyProvider provider)
    {
        provider.GetDependency<IVideoManager>().VideoChanged += OnVideoChanged;
    }

    #region Properties

    public Video? Video { get; private set; }

    #endregion

    private void OnVideoChanged(object? sender, VideoChangedEventArgs e)
    {
        Video = e.Video;
    }
}