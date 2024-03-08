using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.Cleanup.VideoRepoCleanup;

public interface IVideoRepoCleanupService
{
    void CleanupVideo(VideoViewModel video);
}