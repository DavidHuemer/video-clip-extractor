using VideoClipExtractor.Core.Services.VideoProvider;
using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Managers.VideoProviderManager;

/// <summary>
///     Responsible for managing the video provider service.
///     As normal for a manager, this should be a singleton.
/// </summary>
public interface IVideoProviderManager
{
    #region Events

    /// <summary>
    ///     Event that is triggered when a video is added.
    /// </summary>
    event Action<VideoViewModel> VideoAdded;

    #endregion

    /// <summary>
    ///     Sets up a video provider for the given project and repository.
    /// </summary>
    /// <param name="project">The <see cref="Project" /> for which the video provider should be setup</param>
    /// <param name="repository">
    ///     The <see cref="IVideoRepository" />
    ///     that is used by the <see cref="VideoProvider" />
    /// </param>
    void Setup(Project? project, IVideoRepository? repository);

    /// <summary>
    /// Changes the video to the next one.
    /// </summary>
    void Next();
}