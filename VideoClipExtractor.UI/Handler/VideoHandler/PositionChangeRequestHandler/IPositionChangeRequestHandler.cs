using VideoClipExtractor.UI.Controls.VideoPlayer;

namespace VideoClipExtractor.UI.Handler.VideoHandler.PositionChangeRequestHandler;

public interface IPositionChangeRequestHandler
{
    void Setup(IVideoPlayer videoPlayer);
}