using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.VideoRepos.Builder;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.Builder;

/// <summary>
///     Responsible for creating a <see cref="IVideoRepository" /> of a specific type.
/// </summary>
public interface IVideoRepositoryBuilder
{
    IVideoRepository Build(VideoRepositoryBlueprint blueprint);
}