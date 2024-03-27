using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoRepositoryServices.VideoCrawler;

public static class SourceVideoCrawlingHandler
{
    public static bool ShouldCrawl(SourceVideo sourceVideo, List<SourceVideo> sourceVideos) =>
        !sourceVideos.Any(s => s.Path == sourceVideo.Path && s.Checked);
}