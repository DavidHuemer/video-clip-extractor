using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Managers.VideoRepositoryManager;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.Core.Managers.ProjectManager;

[Singleton]
public class ProjectManager(IDependencyProvider provider) : IProjectManager
{
    public void SetOpenedProject(Project project, string path)
    {
        Project = project;
        _path = path;
        ProjectOpened?.Invoke(this, new ProjectOpenedEventArgs(project, path));

        provider.GetDependency<IVideoRepositoryManager>().SetupRepositoryByBlueprint(project.VideoRepositoryBlueprint);
    }

    public event EventHandler<ProjectOpenedEventArgs>? ProjectOpened;

    public void StoreProject()
    {
        if (Project == null || _path == null)
            return;

        provider.GetDependency<IProjectSerializer>().StoreProject(Project, _path);
    }

    #region Properties

    public Project? Project { get; private set; }

    private string? _path;

    #endregion
}