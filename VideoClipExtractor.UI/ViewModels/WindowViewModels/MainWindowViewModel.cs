﻿using BaseUI.Services.DependencyInjection;
using BaseUI.Services.Dialogs;
using BaseUI.Services.RecentlyOpened;
using BaseUI.Services.WindowService;
using BaseUI.ViewModels;
using PropertyChanged;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;
using VideoClipExtractor.Data.Project;
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

        try
        {
            var repoManager = _provider.GetDependency<IVideoRepositoryManager>();
            repoManager.SetupRepository(e.Project.VideoRepositoryBlueprint);

            Project = e.Project;
            _provider.GetDependency<IProjectManager>().SetOpenedProject(e);
        }
        catch (Exception exception)
        {
            _provider.GetDependency<IDialogService>().Show(exception);
        }
    }

    #region Properties

    [DoNotNotify] public MenuViewModel MenuViewModel { get; set; }

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

    #endregion
}