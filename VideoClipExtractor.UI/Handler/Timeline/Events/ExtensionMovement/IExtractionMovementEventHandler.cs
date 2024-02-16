using BaseUI.Basics.FrameworkElementWrapper;

namespace VideoClipExtractor.UI.Handler.Timeline.Events.ExtensionMovement;

public interface IExtractionMovementEventHandler
{
    void Setup(IFrameworkElement timelineControl);
}