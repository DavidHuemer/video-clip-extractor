using BaseUI.Services.Dialogs;
using BaseUI.Services.FileServices;
using BaseUI.Services.Provider.Attributes;
using BaseUI.Services.Provider.DependencyInjection;
using VideoClipExtractor.Core.Managers.ProjectManager;
using VideoClipExtractor.Core.Services.ProjectSerializer;

namespace VideoClipExtractor.UI.Managers.Project.OpenProjectManager;

[Singleton]
public class OpenProjectManager(IDependencyProvider provider) : IOpenProjectManager
{
    private readonly IProjectManager _projectManager = provider.GetDependency<IProjectManager>();

    public void OpenProjectByExplorer()
    {
        var path = provider.GetDependency<IProjectFileExplorer>().GetOpenProjectFilePath();
        OpenProjectByPath(path);
    }

    public void OpenProjectByPath(string projectPath)
    {
        if (string.IsNullOrEmpty(projectPath)) return;

        try
        {
            var project = provider.GetDependency<IProjectSerializer>().LoadProject(projectPath);
            _projectManager.SetOpenedProject(project, projectPath);
        }
        catch (Exception e)
        {
            provider.GetDependency<IDialogService>().Show(e);
        }
    }
}