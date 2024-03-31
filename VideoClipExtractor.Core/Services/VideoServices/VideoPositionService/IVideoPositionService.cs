﻿using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;

public interface IVideoPositionService
{
    event Action<VideoPosition> PositionChangeRequested;

    /// <summary>
    /// Requests a new video position
    /// </summary>
    /// <param name="videoPosition">The requested new video position</param>
    void RequestPositionChange(VideoPosition videoPosition);
}