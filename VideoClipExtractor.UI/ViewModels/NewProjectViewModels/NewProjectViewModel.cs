using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.DependencyInjection;
using BaseUI.ViewModels;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.UI.Managers.ProjectManagers.OpenProjectManager;
using VideoClipExtractor.UI.ViewModels.WindowViewModels.VideoRepositoryExplorer;

namespace VideoClipExtractor.UI.ViewModels.NewProjectViewModels;

/// <summary>
///     ViewModel for the new project panel
/// </summary>
public class NewProjectViewModel(IDependencyProvider provider) : BaseViewModelContainer(provider), INewProjectViewModel
{
    public event Action<Project>? ProjectCreated;

    private void OnVideoRepositorySelected(object? sender, VideoRepositoryBlueprintEventArgs e) =>
        VideoRepositoryBlueprint = e.Blueprint;

    #region Properties

    /// <summary>
    ///     The name of the new project
    /// </summary>
    public string Name { get; set; } = "New Project";

    /// <summary>
    ///     The path where the new project should be stored
    /// </summary>
    public string ProjectPath { get; set; } = string.Empty;

    public VideoRepositoryBlueprint? VideoRepositoryBlueprint { get; set; }

    /// <summary>
    ///     The path where the extracted images and videos should be stored
    /// </summary>
    public string ImageDirectoryPath { get; set; } = string.Empty;

    private bool CanCreate => !string.IsNullOrEmpty(Name) && !string.IsNullOrEmpty(ProjectPath) &&
                              !string.IsNullOrEmpty(ImageDirectoryPath) && VideoRepositoryBlueprint != null;

    #endregion

    #region Commands

    public ICommand BrowseProjectPath => new RelayCommand<string>(DoBrowseProjectPath, _ => true);

    private void DoBrowseProjectPath(string? obj)
    {
        var projectFile = DependencyProvider.GetDependency<IProjectFileExplorer>().GetSaveProjectFilePath();
        if (!string.IsNullOrEmpty(projectFile)) ProjectPath = projectFile;
    }

    public ICommand BrowseVideoRepository => new RelayCommand<string>(DoBrowseVideoRepository, _ => true);

    private void DoBrowseVideoRepository(string? obj)
    {
        var videoRepositoryExplorerVm = ViewModelProvider.Get<IVideoRepositoryExplorerWindowViewModel>();
        videoRepositoryExplorerVm.VideoRepositoryBlueprintSelected += OnVideoRepositorySelected;
        videoRepositoryExplorerVm.ShowDialog();
    }

    public ICommand BrowseImageDirectory => new RelayCommand<string>(DoBrowseImageDirectory, _ => true);

    private void DoBrowseImageDirectory(string? obj)
    {
        var directory = DependencyProvider.GetDependency<IFileExplorer>().GetBrowseDirectoryPath();
        if (!string.IsNullOrEmpty(directory)) ImageDirectoryPath = directory;
    }

    public ICommand CreateProject => new RelayCommand<string>(DoCreateProject, _ => CanCreate);

    private void DoCreateProject(string? obj)
    {
        if (VideoRepositoryBlueprint == null) return;

        var project = new Project
        {
            VideoRepositoryBlueprint = VideoRepositoryBlueprint,
            ImageDirectory = ImageDirectoryPath,
        };

        var projectSerializer = DependencyProvider.GetDependency<IProjectSerializer>();
        projectSerializer.StoreProject(project, ProjectPath);
        DependencyProvider.GetDependency<IOpenProjectManager>().OpenProjectByPath(ProjectPath);
        ProjectCreated?.Invoke(project);
    }

    #endregion
}