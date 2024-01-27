namespace VideoClipExtractor.Data.Project;

public class ProjectOpenedEventArgs(Project project, string path) : EventArgs
{
    public Project Project { get; } = project;

    public string Path { get; } = path;
}