using System.Collections.ObjectModel;
using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

/// <summary>
///     The view model for the videos explorer
/// </summary>
public class VideosExplorerViewModel : BaseViewModel, IVideosExplorerViewModel
{
    public VideosExplorerViewModel(IDependencyProvider provider)
    {
        var videoProviderManager = provider.GetDependency<IVideoProviderManager>();
        videoProviderManager.VideoAdded += OnVideoAdded;
    }

    private void OnVideoAdded(object? sender, VideoEventArgs e)
    {
        var videoViewModel = new VideoViewModel(e.Video);

        Videos.Add(videoViewModel);
        SelectedVideo ??= videoViewModel;
    }

    #region Properties

    /// <summary>
    /// The currently opened videos
    /// </summary>
    public ObservableCollection<VideoViewModel> Videos { get; } = [];

    public VideoViewModel? SelectedVideo { get; set; }

    public int SelectedIndex { get; set; }

    #endregion
}