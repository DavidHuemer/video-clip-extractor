﻿using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.VideoRepos;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoCaching.CacheRunner;

/// <summary>
/// Runs the caching process
/// </summary>
public interface ICacheRunner
{
    bool IsSetup { get; }
    Task<CachedVideo> StoreVideo(SourceVideo sourcePath);

    void Setup(Project project, IVideoRepository repository);
}