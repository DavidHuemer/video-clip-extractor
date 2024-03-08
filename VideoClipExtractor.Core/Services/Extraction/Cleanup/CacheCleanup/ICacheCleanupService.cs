using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.Extraction.Cleanup.CacheCleanup;

public interface ICacheCleanupService
{
    void CleanUpCachedVideo(VideoViewModel video);
}