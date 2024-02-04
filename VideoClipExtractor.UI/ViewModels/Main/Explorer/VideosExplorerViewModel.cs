using System.Collections.ObjectModel;
using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

/// <summary>
/// The view model for the videos explorer
/// </summary>
public class VideosExplorerViewModel : BaseViewModel
{
    #region Private Fields

    private readonly IVideoManager _videoManager;

    #endregion

    public VideosExplorerViewModel(IDependencyProvider provider)
    {
        var videoProviderManager = provider.GetDependency<IVideoProviderManager>();
        videoProviderManager.VideoAdded += OnVideoAdded;

        _videoManager = provider.GetDependency<IVideoManager>();
    }

    private void OnVideoAdded(object? sender, VideoEventArgs e)
    {
        Videos.Add(e.Video);
        SelectedVideo ??= e.Video;
    }

    #region Properties

    public ObservableCollection<Video> Videos { get; } = [];

    private Video? _selectedVideo;

    public Video? SelectedVideo
    {
        get => _selectedVideo;
        set
        {
            _selectedVideo = value;
            _videoManager.Video = value;
            OnPropertyChanged();
        }
    }

    #endregion
}