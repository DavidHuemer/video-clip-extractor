namespace VideoClipExtractor.Data.Project;

public class ProjectEventArgs(Project project) : EventArgs
{
    public Project Project { get; init; } = project;
}