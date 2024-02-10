using System.Windows.Input;

namespace BaseUI.Events;

public class MouseButtonEventArgsWrapper(MouseButton button, Func<IInputElement, Point> getPosition)
    : MouseEventArgsWrapper(getPosition)
{
    public MouseButton Button { get; } = button;
}