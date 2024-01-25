namespace VideoClipExtractor.Data.VideoRepos.Builder;

public class VideoRepositoryBlueprintEventArgs(VideoRepositoryBlueprint blueprint) : EventArgs
{
    public VideoRepositoryBlueprint Blueprint { get; } = blueprint;
}