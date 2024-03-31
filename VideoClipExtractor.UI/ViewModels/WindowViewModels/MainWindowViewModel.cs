using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.Services.WindowService;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.UI.ViewModels.Main;
using VideoClipExtractor.UI.ViewModels.Main.ControlPanel;
using VideoClipExtractor.UI.ViewModels.Menu;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.WelcomeWindow;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

[Singleton]
public class MainWindowViewModel : WindowViewModel
{
    #region Private Fields

    private readonly IDependencyProvider _provider;

    #endregion

    public MainWindowViewModel(IDependencyProvider provider) : base(provider)
    {
        _provider = provider;
        MainControlViewModel = ViewModelProvider.Get<IMainControlViewModel>();
        MenuViewModel = ViewModelProvider.Get<IMenuViewModel>();
        ControlPanelViewModel = ViewModelProvider.Get<IControlPanelViewModel>();
    }

    protected override void SetupEvents(IWindow window)
    {
        base.SetupEvents(window);
        window.ContentRendered += WindowOnContentRendered;
    }

    private void WindowOnContentRendered(object? sender, EventArgs e)
    {
        var welcomeWindow = ViewModelProvider.Get<IWelcomeWindowViewModel>();
        welcomeWindow.ShowDialog();
    }

    /// <summary>
    ///     Is called when a project is chosen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">Event that contains the opened project</param>
    private void OnProjectOpened(object? sender, ProjectOpenedEventArgs e)
    {
        // var recentlyOpenedFilesService = _provider.GetDependency<IRecentlyOpenedFilesService>();
        // recentlyOpenedFilesService.AddFile(e.Path);
        //
        // try
        // {
        //     var repoManager = _provider.GetDependency<IVideoRepositoryManager>();
        //     repoManager.SetupRepository(e.Project.VideoRepositoryBlueprint);
        //
        //     Project = e.Project;
        //     _provider.GetDependency<IProjectManager>().SetOpenedProject(e);
        //
        //     _provider.GetDependency<IVideoProviderManager>().Setup(e.Project, repoManager.VideoRepository!);
        //
        //     if (Project.Videos.Count == 0) ShowSetupVideos();
        // }
        // catch (Exception exception)
        // {
        //     _provider.GetDependency<IDialogService>().Show(exception);
        // }
    }

    // private void ShowSetupVideos()
    // {
    //     var setupVideosWindow = new VideosSetupWindowViewModel(_provider, true);
    //     setupVideosWindow.Show();
    // }

    #region Properties

    [DoNotNotify] public IMenuViewModel MenuViewModel { get; }

    [DoNotNotify] public IMainControlViewModel MainControlViewModel { get; }

    private Project? _project;

    private Project? Project
    {
        get => _project;
        set { _project = value; }
    }

    [DoNotNotify] public IControlPanelViewModel ControlPanelViewModel { get; }

    #endregion
}