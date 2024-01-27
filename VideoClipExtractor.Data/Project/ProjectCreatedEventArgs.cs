namespace VideoClipExtractor.Data.Project;

public class ProjectCreatedEventArgs(Project project, string path) : EventArgs
{
    public Project Project { get; } = project;

    public string Path { get; } = path;
}