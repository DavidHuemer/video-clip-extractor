using System.Windows;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.NavigationEventHandler;

public interface ITimelineNavigationEventHandler
{
    void MarkerMouseButtonDown(Point point);

    void MovementMouseButtonDown(Point point);

    void MouseButtonUp();

    void MouseMove(Point position);
}