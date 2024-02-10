using BaseUI.Basics.FrameworkElementWrapper;

namespace BaseUI.Events;

public class MouseEventArgsWrapper(Func<IInputElement, Point> getPosition) : EventArgs
{
    private Func<IInputElement, Point> GetPositionAction { get; } = getPosition;

    public Point GetPosition(IFrameworkElement frameworkWrapper) => GetPositionAction(frameworkWrapper.Element);
}