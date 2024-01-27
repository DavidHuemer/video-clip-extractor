using BaseUI.Services.DependencyInjection;
using BaseUI.Services.RecentlyOpened;
using BaseUI.Services.WindowService;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

public class MainWindowViewModel : WindowViewModel
{
    #region Private Fields

    private readonly IDependencyProvider _provider;

    #endregion

    public MainWindowViewModel(IDependencyProvider provider)
    {
        _provider = provider;
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
    /// Is called when a project is chosen
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e">Event that contains the opened project</param>
    private void OnProjectOpened(object? sender, ProjectOpenedEventArgs e)
    {
        // TODO: Open project
        var recentlyOpenedFilesService = _provider.GetDependency<IRecentlyOpenedFilesService>();
        recentlyOpenedFilesService.AddFile(e.Path);

        var repoManager = _provider.GetDependency<IVideoRepositoryManager>();
        repoManager.SetupRepository(e.Project.VideoRepositoryBlueprint);
    }
}