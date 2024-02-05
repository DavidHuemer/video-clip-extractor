using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Tests.Basics.Data;

public static class ProjectExamples
{
    public static Project GetExampleProject()
    {
        return new Project
        {
            ImageDirectory = "",
            Videos = [],
            VideoRepositoryBlueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, ""),
        };
    }
}