using VideoClipExtractor.Data.Project;

namespace VideoClipExtractor.Core.Services.ProjectSerializer;

/// <summary>
/// Responsible for serializing and deserializing <see cref="Project"/>s.
/// </summary>
public interface IProjectSerializer
{
    /// <summary>
    /// Stores the given <see cref="Project"/> at the given path.
    /// </summary>
    /// <param name="project">The project that should be stored</param>
    /// <param name="path">The path where the project should be stored</param>
    void StoreProject(Project project, string path);

    /// <summary>
    /// Loads the project from a given path.
    /// </summary>
    /// <param name="path">The path from which the project should be loaded</param>
    /// <returns>The loaded project</returns>
    Project LoadProject(string path);
}