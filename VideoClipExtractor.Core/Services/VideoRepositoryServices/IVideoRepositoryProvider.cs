using VideoClipExtractor.Data.VideoRepos;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices;

/// <summary>
/// Responsible for providing VideoRepositories
/// </summary>
public interface IVideoRepositoryProvider
{
    IEnumerable<VideoRepositoryDrive> GetDrives();
}