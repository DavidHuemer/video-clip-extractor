using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.VideoRepos.Builder;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Data.Project;

public class Project
{
    /// <summary>
    /// The blueprint for the video repository.
    /// <para>Is used to generate a <see cref="IVideoRepository"/>.</para>
    /// </summary>
    public required VideoRepositoryBlueprint VideoRepositoryBlueprint { get; init; }

    /// <summary>
    /// The directory where the images are stored
    /// </summary>
    public required string ImageDirectory { get; init; }

    /// <summary>
    /// The list of videos that are part of the project
    /// </summary>
    public List<SourceVideo> Videos { get; set; } = [];

    /// <summary>
    /// The list of videos that are currently being worked on
    /// </summary>
    public List<VideoViewModel> WorkingVideos { get; set; } = [];

    public override bool Equals(object? obj)
    {
        return obj is Project project &&
               EqualityComparer<VideoRepositoryBlueprint>.Default.Equals(VideoRepositoryBlueprint,
                   project.VideoRepositoryBlueprint) &&
               ImageDirectory == project.ImageDirectory &&
               Videos.SequenceEqual(project.Videos) &&
               WorkingVideos.SequenceEqual(project.WorkingVideos);
    }

    public override int GetHashCode() => 0;
}