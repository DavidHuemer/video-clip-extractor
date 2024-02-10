using BaseUI.Data;

namespace VideoClipExtractor.UI.Handler.Timeline.Events;

public interface ITimelineZoomEventHandler
{
    void Zoom(ZoomDirection zoomDirection);
}