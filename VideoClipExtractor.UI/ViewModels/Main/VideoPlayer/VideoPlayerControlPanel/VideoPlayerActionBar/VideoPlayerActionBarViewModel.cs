using System.Windows.Input;

namespace VideoClipExtractor.UI.ViewModels.Main.VideoPlayer.VideoPlayerControlPanel.VideoPlayerActionBar;

[Transient]
public class VideoPlayerActionBarViewModel : BaseViewModelContainer, IVideoPlayerActionBarViewModel
{
    private readonly IProjectManager _projectManager;
    private readonly IVideoProviderManager _videoProviderManager;

    public VideoPlayerActionBarViewModel(IDependencyProvider provider) : base(provider)
    {
        _projectManager = provider.GetDependency<IProjectManager>();
        _videoProviderManager = provider.GetDependency<IVideoProviderManager>();
        VideoExplorer = ViewModelProvider.Get<IVideosExplorerViewModel>();
    }

    public IVideosExplorerViewModel VideoExplorer { get; }

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
        var project = _projectManager.Project;
        if (project != null && VideoExplorer.SelectedVideo != null &&
            VideoExplorer.SelectedVideo.VideoStatus == VideoStatus.Unset)
        {
            project.WorkingVideos.Add(VideoExplorer.SelectedVideo);
        }

        VideoExplorer.SelectedVideo!.VideoStatus = status;
        AccessNext();
    }

    #endregion
}