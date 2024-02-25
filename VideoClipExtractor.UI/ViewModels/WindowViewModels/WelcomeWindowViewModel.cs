using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.UI.ViewModels.NewProjectViewModels;
using VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

namespace VideoClipExtractor.UI.ViewModels.WindowViewModels;

[Singleton]
public class WelcomeWindowViewModel : WindowViewModel
{
    public WelcomeWindowViewModel(IDependencyProvider provider)
    {
        _provider = provider;
        _welcomeViewModel = new WelcomeViewModel(_provider);
        _welcomeViewModel.NewProjectRequested += NewProjectRequested;
        _welcomeViewModel.OpenProjectRequested += OpenProjectRequested;
        _welcomeViewModel.OpenRecentProjectRequested += OnOpenRecentProjectRequested;
        CurrentControl = _welcomeViewModel;
    }

    #region Events

    public event EventHandler<ProjectOpenedEventArgs>? ProjectOpened;

    #endregion

    #region Private Fields

    private readonly IDependencyProvider _provider;

    private readonly WelcomeViewModel _welcomeViewModel;

    #endregion

    #region Properties

    public bool ShowBackButton { get; set; }

    public BaseViewModel CurrentControl { get; private set; }

    #endregion

    #region Welcome Requests

    private void NewProjectRequested(object? sender, EventArgs e)
    {
        var newProjectViewModel = new NewProjectViewModel(_provider);
        newProjectViewModel.ProjectCreated += ProjectCreated;
        CurrentControl = newProjectViewModel;
        ShowBackButton = true;
    }

    private void ProjectCreated(object? sender, ProjectCreatedEventArgs e)
    {
        OpenProject(e.Project, e.Path);
    }

    private void OpenProjectRequested(object? sender, EventArgs e)
    {
        var path = _provider.GetDependency<IProjectFileExplorer>().GetOpenProjectFilePath();
        OpenProjectByPath(path);
    }

    private void OnOpenRecentProjectRequested(object? sender, OpenRecentlyOpenedEventArgs e)
    {
        OpenProjectByPath(e.RecentlyOpenedPath);
    }

    private void OpenProjectByPath(string path)
    {
        if (string.IsNullOrEmpty(path)) return;

        var project = _provider.GetDependency<IProjectSerializer>().LoadProject(path);
        OpenProject(project, path);
    }

    private void OpenProject(Project project, string path)
    {
        CloseWindow();
        ProjectOpened?.Invoke(this, new ProjectOpenedEventArgs(project, path));
    }

    #endregion

    #region Commands

    public ICommand GoBack => new RelayCommand<string>(DoGoBack, _ => true);

    private void DoGoBack(string? obj)
    {
        CurrentControl = _welcomeViewModel;
        ShowBackButton = false;
    }

    #endregion
}