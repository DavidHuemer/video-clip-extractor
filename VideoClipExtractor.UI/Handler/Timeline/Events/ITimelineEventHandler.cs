using BaseUI.Data;

namespace VideoClipExtractor.UI.Handler.Timeline.Events;

public interface ITimelineEventHandler
{
    void Zoom(ZoomDirection zoomDirection);
}