using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.Provider.ViewModelProvider;
using BaseUI.ViewModels;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

/// <summary>
///     Viewmodel for the navigation part of the video player.
///     Choosing next video, export, ...
/// </summary>
[UsedImplicitly]
public class VideoPlayerNavigationViewModel : BaseViewModel, IVideoPlayerNavigationViewModel
{
    #region Private Fields

    private readonly IVideoProviderManager _videoProviderManager;

    #endregion

    public VideoPlayerNavigationViewModel(IDependencyProvider provider)
    {
        _videoProviderManager = provider.GetDependency<IVideoProviderManager>();
        var viewModelProvider = provider.GetDependency<IViewModelProvider>();
        VideoExplorer = viewModelProvider.GetViewModel<IVideosExplorerViewModel>();
    }

    #region Properties

    private IVideosExplorerViewModel VideoExplorer { get; }

    #endregion

    #region Commands

    public ICommand Previous => new RelayCommand<string>(DoPrevious,
        _ => VideoExplorer is { SelectedVideo: not null, SelectedIndex: > 0 });

    private void DoPrevious(string? obj) => VideoExplorer.SelectedIndex--;

    public ICommand Skip => new RelayCommand<string>(DoSkip, _ => VideoExplorer.SelectedVideo != null);

    private void DoSkip(string? obj)
    {
        VideoExplorer.SelectedVideo!.VideoStatus = Data.Videos.VideoStatus.Skipped;
        _videoProviderManager.Next();
    }

    public ICommand Finish => new RelayCommand<string>(DoFinish, _ => VideoExplorer.SelectedVideo != null);

    private void DoFinish(string? obj)
    {
        VideoExplorer.SelectedVideo!.VideoStatus = Data.Videos.VideoStatus.ReadyForExport;
        _videoProviderManager.Next();
    }

    #endregion
}