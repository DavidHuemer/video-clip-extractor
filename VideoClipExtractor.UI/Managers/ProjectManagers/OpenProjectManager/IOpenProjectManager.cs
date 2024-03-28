namespace VideoClipExtractor.UI.Managers.ProjectManagers.OpenProjectManager;

/// <summary>
/// Responsible for opening a project.
/// </summary>
public interface IOpenProjectManager
{
    /// <summary>
    /// Opens an explorer to select a project.
    /// After the project is selected, the project is opened.
    /// </summary>
    Task OpenProjectByExplorer();

    /// <summary>
    /// Opens a project by a given path.
    /// </summary>
    /// <param name="projectPath">The path to the project</param>
    Task OpenProjectByPath(string projectPath);
}