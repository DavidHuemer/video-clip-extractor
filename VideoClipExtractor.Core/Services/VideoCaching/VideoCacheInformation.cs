using VideoClipExtractor.Data.VideoRepos;

namespace VideoClipExtractor.Core.Services.VideoCaching;

internal record VideoCacheInformation(IVideoRepository Repository, string LocalCachePath);