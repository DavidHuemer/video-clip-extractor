using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

/// <summary>
///     Viewmodel for the navigation part of the video player.
///     Choosing next video, export, ...
/// </summary>
[Singleton]
public class VideoPlayerNavigationViewModel : BaseViewModel, IVideoPlayerNavigationViewModel
{
    #region Private Fields

    private readonly IVideoProviderManager _videoProviderManager;

    #endregion

    public VideoPlayerNavigationViewModel(IDependencyProvider provider)
    {
        _videoProviderManager = provider.GetDependency<IVideoProviderManager>();
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        VideoExplorer = viewModelProvider.Get<IVideosExplorerViewModel>();
    }

    #region Properties

    public IVideosExplorerViewModel VideoExplorer { get; }

    #endregion

    private void AccessNext()
    {
        if (VideoExplorer.SelectedIndex < VideoExplorer.Videos.Count - 1)
        {
            VideoExplorer.SelectedIndex++;
        }
        else
        {
            _videoProviderManager.Next();
        }
    }

    #region Commands

    public ICommand Previous => new RelayCommand<string>(DoPrevious,
        _ => VideoExplorer is { SelectedVideo: not null, SelectedIndex: > 0 });

    private void DoPrevious(string? obj) => VideoExplorer.SelectedIndex--;

    public ICommand Skip => new RelayCommand<string>(DoSkip, _ => VideoExplorer.SelectedVideo != null);

    private void DoSkip(string? obj) => SetVideoStatus(VideoStatus.Skipped);

    public ICommand Finish => new RelayCommand<string>(DoFinish, _ => VideoExplorer.SelectedVideo != null);

    private void DoFinish(string? obj) => SetVideoStatus(VideoStatus.ReadyForExport);

    private void SetVideoStatus(VideoStatus status)
    {
        VideoExplorer.SelectedVideo!.VideoStatus = status;
        AccessNext();
    }

    #endregion
}