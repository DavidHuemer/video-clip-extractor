using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.Core.Managers.ProjectManager;

/// <summary>
///     Responsible for managing the opened project.
/// </summary>
public interface IProjectManager
{
    #region Properties

    public Project? Project { get; }

    #endregion

    #region Events

    event EventHandler<ProjectOpenedEventArgs>? ProjectOpened;

    #endregion

    #region Storage

    /// <summary>
    ///     Stores the currently opened project.
    /// </summary>
    void StoreProject();

    #endregion

    #region Set Project

    /// <summary>
    ///     Sets the currently opened project.
    /// </summary>
    /// <param name="project">The open project</param>
    /// <param name="path">The path of the project that is currently opened</param>
    void SetOpenedProject(Project project, string path);

    #endregion
}