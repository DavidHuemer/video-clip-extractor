using JetBrains.Annotations;
using VideoClipExtractor.Data.Events;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;

[UsedImplicitly]
public class VideoPositionService : IVideoPositionService
{
    public event EventHandler<VideoPositionEventArgs>? PositionChangeRequested;

    public void RequestPositionChange(VideoPosition videoPosition) =>
        PositionChangeRequested?.Invoke(this, new VideoPositionEventArgs(videoPosition));
}