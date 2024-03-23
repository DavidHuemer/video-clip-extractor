using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoProvider.CachedVideosService;

public interface ICachedVideosService
{
    bool IsVideoCached { get; }
    void Add(CachedVideo video);

    CachedVideo GetNextCachedVideo();
}