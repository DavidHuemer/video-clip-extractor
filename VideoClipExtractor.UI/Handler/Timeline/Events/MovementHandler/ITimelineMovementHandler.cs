using System.Windows;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.MovementHandler;

public interface ITimelineMovementHandler
{
    void StartMovement(Point position);

    void Move(Point position);

    void EndMovement();
}