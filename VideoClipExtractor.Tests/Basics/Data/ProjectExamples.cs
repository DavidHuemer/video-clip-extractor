using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Tests.Basics.Data;

public static class ProjectExamples
{
    public static Project GetExampleProject()
    {
        return new Project()
        {
            ImageDirectory = "",
            Videos = new List<SourceVideo>(),
            VideoRepositoryBlueprint = new VideoRepositoryBlueprint(VideoRepositoryType.Pc, "")
        };
    }
}