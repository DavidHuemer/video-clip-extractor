using System.Collections.ObjectModel;
using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.WindowService;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Data.Videos.Events;
using VideoClipExtractor.UI.ViewModels.WindowViewModels;

namespace VideoClipExtractor.UI.ViewModels.Main.Explorer;

/// <summary>
///     The view model for the videos explorer
/// </summary>
public class VideosExplorerViewModel : BaseViewModel, IVideosExplorerViewModel
{
    private readonly IDependencyProvider _provider;

    public VideosExplorerViewModel(IDependencyProvider provider)
    {
        _provider = provider;
        var videoProviderManager = provider.GetDependency<IVideoProviderManager>();
        videoProviderManager.VideoAdded += OnVideoAdded;
    }

    private void OnVideoAdded(object? sender, VideoEventArgs e)
    {
        var videoViewModel = new VideoViewModel(e.Video);

        Videos.Add(videoViewModel);
        SelectedVideo = videoViewModel;
    }

    #region Properties

    /// <summary>
    /// The currently opened videos
    /// </summary>
    public ObservableCollection<VideoViewModel> Videos { get; } = [];

    public VideoViewModel? SelectedVideo { get; set; }

    public int SelectedIndex { get; set; }

    #endregion

    #region Commands

    public ICommand ExportVideos => new RelayCommand<string>(DoExportVideos, _ => true);

    private void DoExportVideos(string? obj)
    {
        var windowService = _provider.GetDependency<IWindowService>();
        var videoRepositoryExplorerVm = new ExtractionWindowViewModel(_provider);
        videoRepositoryExplorerVm.ShowDialog(windowService);
    }

    #endregion
}