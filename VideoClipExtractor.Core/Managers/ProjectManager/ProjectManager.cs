using BaseUI.Services.Provider.DependencyInjection;
using JetBrains.Annotations;
using VideoClipExtractor.Core.Services.ProjectSerializer;
using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.Core.Managers.ProjectManager;

[UsedImplicitly]
public class ProjectManager(IDependencyProvider provider) : IProjectManager
{
    public void SetOpenedProject(Project project, string path)
    {
        Project = project;
        Path = path;
    }

    public void SetOpenedProject(ProjectOpenedEventArgs e)
    {
        SetOpenedProject(e.Project, e.Path);
    }

    public void StoreProject()
    {
        if (Project == null || Path == null)
            return;

        provider.GetDependency<IProjectSerializer>().StoreProject(Project, Path);
    }

    #region Properties

    public string? Path { get; private set; }
    public Project? Project { get; private set; }

    #endregion
}