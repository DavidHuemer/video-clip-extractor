using BaseUI.Data;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.ZoomEventHandler;

/// <summary>
/// Responsible for handling zoom events of the timeline control
/// </summary>
public interface ITimelineZoomEventHandler : IBaseTimelineEventHandler
{
    void Zoom(ZoomDirection zoomDirection);
}