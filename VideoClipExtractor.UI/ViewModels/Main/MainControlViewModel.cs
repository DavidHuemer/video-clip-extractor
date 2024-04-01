using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoManager;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.ViewModels.Main;

/// <summary>
///     View model for the main control.
/// </summary>
public class MainControlViewModel : BaseViewModelContainer, IMainControlViewModel
{
    private readonly IVideoManager _videoManager;

    public MainControlViewModel(IDependencyProvider provider) : base(provider)
    {
        ExplorerVm = ViewModelProvider.Get<IVideosExplorerViewModel>();
        VideoPlayerVm = ViewModelProvider.Get<IVideoPlayerViewModel>();
        ControlPanelVm = ViewModelProvider.Get<IControlPanelViewModel>();

        var projectManager = provider.GetDependency<IProjectManager>();

        projectManager.ProjectChanged += (project) => { ExplorerVm.Project = project; };
        ExplorerVm.Project = projectManager.Project;

        _videoManager = provider.GetDependency<IVideoManager>();
        SetupVideoChange();
    }

    public IVideosExplorerViewModel ExplorerVm { get; }

    public IControlPanelViewModel ControlPanelVm { get; }

    public IVideoPlayerViewModel VideoPlayerVm { get; }

    private void SetupVideoChange()
    {
        ExplorerVm.PropertyChanged += (_, args) =>
        {
            if (args.PropertyName == nameof(IVideosExplorerViewModel.SelectedVideo))
                OnVideoChanged(ExplorerVm.SelectedVideo);
        };
    }

    private void OnVideoChanged(VideoViewModel? video)
    {
        VideoPlayerVm.Video = video;
        ControlPanelVm.Video = video;
        _videoManager.Video = video;
    }
}