﻿using VideoClipExtractor.Data.Project;
using VideoClipExtractor.Data.Videos;

namespace VideoClipExtractor.Core.Services.VideoProvider.RemainingVideosService;

/// <summary>
/// Responsible for handling the remaining videos
/// </summary>
public interface IRemainingVideosService
{
    int RemainingVideosCount { get; }
    void Setup(Project project);

    SourceVideo GetNextVideo();
}