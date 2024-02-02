using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Data.Project;

public class Project
{
    public required VideoRepositoryBlueprint VideoRepositoryBlueprint { get; init; }

    public List<SourceVideo> Videos { get; set; } = [];

    public required string ImageDirectory { get; set; }
}