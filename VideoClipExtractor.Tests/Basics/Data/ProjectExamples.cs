using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Data.Videos;
using VideoClipExtractor.Tests.Basics.Data.VideoExamples;

namespace VideoClipExtractor.Tests.Basics.Data;

public static class ProjectExamples
{
    /// <summary>
    /// The path to the image directory
    /// </summary>
    public const string ImageDirectory = @"C:\Images";

    public static Project GetExampleProject(string imageDirectory = ImageDirectory,
        List<SourceVideo>? sourceVideos = null,
        VideoRepositoryBlueprint? videoRepositoryBlueprint = null,
        List<VideoViewModel>? workingVideos = null)
    {
        return new Project
        {
            ImageDirectory = imageDirectory,
            Videos = sourceVideos ?? [],
            VideoRepositoryBlueprint =
                videoRepositoryBlueprint ?? VideoRepositoryExamples.GetVideoRepositoryBlueprintExample(),
            WorkingVideos = workingVideos ?? [],
        };
    }

    public static Project GetEmptyProject() => GetExampleProject();

    /// <summary>
    /// Returns a project with a realistic amount of videos.
    /// </summary>
    /// <returns>Realistic project</returns>
    public static Project GetRealisticProject()
    {
        var realisticSourceVideos = SourceVideoExamples.GetRealisticSourceVideos();
        var realisticWorkingVideos = VideoExamples.VideoExamples.GetRealisticVideoViewModels();
        return GetExampleProject(sourceVideos: realisticSourceVideos, workingVideos: realisticWorkingVideos);
    }
}