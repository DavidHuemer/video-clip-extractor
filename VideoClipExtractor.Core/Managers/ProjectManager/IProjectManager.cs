using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.Core.Managers.ProjectManager;

/// <summary>
///     Responsible for managing the opened project.
/// </summary>
public interface IProjectManager
{
    #region Storage

    /// <summary>
    ///     Stores the currently opened project.
    /// </summary>
    void StoreProject();

    #endregion

    #region Properties

    public string? Path { get; }

    public Project? Project { get; }

    #endregion

    #region Set Project

    /// <summary>
    ///     Sets the currently opened project.
    /// </summary>
    /// <param name="project">The open project</param>
    /// <param name="path">The path of the project that is currently opened</param>
    void SetOpenedProject(Project project, string path);

    /// <summary>
    ///     Sets the currently opened project by using the <see cref="ProjectOpenedEventArgs" /> event.
    /// </summary>
    /// <param name="e">
    ///     The <see cref="ProjectOpenedEventArgs" /> event
    ///     that contains the currently opened project
    /// </param>
    void SetOpenedProject(ProjectOpenedEventArgs e);

    #endregion
}