using System.Windows;
using BaseUI.Basics.FrameworkElementWrapper;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.MarkerEventHandler;

public interface ITimelineMarkerEventHandler
{
    void StartMarkerMovement(Point position);

    void Setup(IFrameworkElement timelineControl);

    void StopMarkerMovement();
}