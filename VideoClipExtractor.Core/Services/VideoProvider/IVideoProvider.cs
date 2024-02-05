using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos.Events;

namespace VideoClipExtractor.Core.Services.VideoProvider;

public interface IVideoProvider
{
    event EventHandler<VideoEventArgs>? VideoAdded;

    void Setup(Project project, IVideoRepository repository);

    /// <summary>
    ///     Requests the next video.
    /// </summary>
    void Next();
}