using System.Windows;
using BaseUI.Basics.FrameworkElementWrapper;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.MovementEventHandler;

public interface ITimelineMovementEventHandler
{
    void StartMovement(Point position);

    void Setup(IFrameworkElement videoPlayer);
    void StopMovement();
}