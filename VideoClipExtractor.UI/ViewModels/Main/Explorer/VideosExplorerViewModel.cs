using System.Collections.ObjectModel;
using BaseUI.Services.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

/// <summary>
/// The view model for the videos explorer
/// </summary>
public class VideosExplorerViewModel : BaseViewModel
{
    public VideosExplorerViewModel(IDependencyProvider provider)
    {
        var videoProviderManager = provider.GetDependency<IVideoProviderManager>();
        videoProviderManager.VideoAdded += OnVideoAdded;
    }

    private void OnVideoAdded(object? sender, VideoEventArgs e)
    {
        Videos.Add(e.Video);
        SelectedVideo ??= e.Video;
    }

    #region Properties

    public ObservableCollection<Video> Videos { get; } = [];

    public Video? SelectedVideo { get; set; }

    #endregion
}