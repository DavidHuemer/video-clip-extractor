using BaseUI.Basics.FrameworkElementWrapper;

namespace BaseUI.Basics.MouseCursorHandler;

public interface IMouseCursorHandler
{
    void SetCursorPosition(Point position);
    Point GetMousePosition(IFrameworkElement element);
}