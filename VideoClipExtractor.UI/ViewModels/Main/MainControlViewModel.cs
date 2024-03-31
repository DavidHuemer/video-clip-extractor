using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;
using VideoClipExtractor.UI.ViewModels.Main.Explorer;
using VideoClipExtractor.UI.ViewModels.Main.VideoPlayer;

namespace VideoClipExtractor.UI.ViewModels.Main;

/// <summary>
///     View model for the main control.
/// </summary>
public class MainControlViewModel : BaseViewModelContainer, IMainControlViewModel
{
    public MainControlViewModel(IDependencyProvider provider) : base(provider)
    {
        ExplorerVm = ViewModelProvider.Get<IVideosExplorerViewModel>();
        VideoPlayerVm = ViewModelProvider.Get<IVideoPlayerViewModel>();
        ControlPanelVm = ViewModelProvider.Get<IControlPanelViewModel>();

        var projectManager = provider.GetDependency<IProjectManager>();

        projectManager.ProjectChanged += (project) => { ExplorerVm.Project = project; };
        ExplorerVm.Project = projectManager.Project;
    }

    public IVideosExplorerViewModel ExplorerVm { get; }

    public IControlPanelViewModel ControlPanelVm { get; }

    public IVideoPlayerViewModel VideoPlayerVm { get; }
}