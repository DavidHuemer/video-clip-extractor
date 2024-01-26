using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.Manager;

/// <summary>
/// Responsible for managing a <see cref="IVideoRepository"/>
/// </summary>
public interface IVideoRepositoryManager
{
    public IVideoRepository? VideoRepository { get; set; }

    /// <summary>
    /// Sets the <see cref="IVideoRepository"/> by using the given <see cref="VideoRepositoryBlueprint"/>
    /// </summary>
    /// <param name="blueprint">The template that holds the required information to create the
    /// <see cref="IVideoRepository"/></param>
    void SetupRepository(VideoRepositoryBlueprint blueprint);
}