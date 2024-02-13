using BaseUI.Basics.FrameworkElementWrapper;

namespace VideoClipExtractor.UI.Handler.Timeline.Events;

public interface IBaseTimelineEventHandler
{
    void Setup(IFrameworkElement timelineControl);
}