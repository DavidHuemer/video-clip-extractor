namespace BaseUI.Events;

public class MouseWheelEventArgsWrapper(int delta) : EventArgs
{
    public int Delta { get; } = delta;
}