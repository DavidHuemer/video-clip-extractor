using BaseUI.Services.Provider.Attributes;
using VideoClipExtractor.Data.UI.Video;

namespace VideoClipExtractor.Core.Services.VideoServices.VideoPositionService;

[Singleton]
public class VideoPositionService : IVideoPositionService
{
    public event Action<VideoPosition>? PositionChangeRequested;

    public void RequestPositionChange(VideoPosition videoPosition) =>
        PositionChangeRequested?.Invoke(videoPosition);
}