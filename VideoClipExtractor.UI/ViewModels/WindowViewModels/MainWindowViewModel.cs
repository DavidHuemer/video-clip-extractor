using BaseUI.Services.Dialogs;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.RecentlyOpened;
using BaseUI.Services.WindowService;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Managers.VideoProviderManager;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.UI.ViewModels.Main;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;
using VideoClipExtractor.UI.ViewModels.Menu;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

public class MainWindowViewModel : WindowViewModel
{
    #region Private Fields

    private readonly IDependencyProvider _provider;

    #endregion

    public MainWindowViewModel(IDependencyProvider provider)
    {
        _provider = provider;
        MenuViewModel = new MenuViewModel(provider);
        MainControlViewModel = new MainControlViewModel(provider);
    }

    protected override void SetupEvents(IWindow window)
    {
        base.SetupEvents(window);
        window.ContentRendered += WindowOnContentRendered;
    }

    private void WindowOnContentRendered(object? sender, EventArgs e)
    {
        var windowService = _provider.GetDependency<IWindowService>();
        var welcomeWindow = new WelcomeWindowViewModel(_provider);
        welcomeWindow.ProjectOpened += OnProjectOpened;
        welcomeWindow.ShowDialog(windowService);
    }

    /// <summary>
    ///     Is called when a project is chosen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">Event that contains the opened project</param>
    private void OnProjectOpened(object? sender, ProjectOpenedEventArgs e)
    {
        var recentlyOpenedFilesService = _provider.GetDependency<IRecentlyOpenedFilesService>();
        recentlyOpenedFilesService.AddFile(e.Path);

        try
        {
            var repoManager = _provider.GetDependency<IVideoRepositoryManager>();
            repoManager.SetupRepository(e.Project.VideoRepositoryBlueprint);

            Project = e.Project;
            _provider.GetDependency<IProjectManager>().SetOpenedProject(e);

            _provider.GetDependency<IVideoProviderManager>().Setup(e.Project, repoManager.VideoRepository!);

            if (Project.Videos.Count == 0) ShowSetupVideos();
        }
        catch (Exception exception)
        {
            _provider.GetDependency<IDialogService>().Show(exception);
        }
    }

    private void ShowSetupVideos()
    {
        var windowService = _provider.GetDependency<IWindowService>();
        var setupVideosWindow = new VideosSetupWindowViewModel(_provider, true);
        setupVideosWindow.Show(windowService);
    }

    #region Properties

    [DoNotNotify] public MenuViewModel MenuViewModel { get; set; }

    [DoNotNotify] public MainControlViewModel MainControlViewModel { get; set; }

    private Project? _project;

    private Project? Project
    {
        get => _project;
        set
        {
            _project = value;
            MenuViewModel.Project = value;
        }
    }

    [DoNotNotify]
    public IControlPanelViewModel ControlPanelViewModel => MainControlViewModel.VideoPlayerVm.ControlPanelViewModel;

    #endregion
}