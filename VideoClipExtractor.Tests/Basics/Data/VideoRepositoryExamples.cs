using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Tests.Basics.Data;

public static class VideoRepositoryExamples
{
    public const string VideoRepositoryPath = @"C:\SourceVideos";

    public static VideoRepositoryBlueprint GetVideoRepositoryBlueprintExample()
    {
        return new VideoRepositoryBlueprint(VideoRepositoryType.Pc, VideoRepositoryPath);
    }
}