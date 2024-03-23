using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.Core.Exceptions;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoProvider.CachedVideosService;

[Transient]
public class CachedVideosService : ICachedVideosService
{
    private readonly Queue<CachedVideo> _cachedVideos = new();

    public void Add(CachedVideo video) =>
        _cachedVideos.Enqueue(video);

    public CachedVideo GetNextCachedVideo()
    {
        if (!IsVideoCached)
            throw new CachedVideosEmptyException();

        return _cachedVideos.Dequeue();
    }

    public bool IsVideoCached => _cachedVideos.Count > 0;
}