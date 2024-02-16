using BaseUI.Events;

namespace BaseUI.Basics.FrameworkElementWrapper;

public interface IFrameworkElement
{
    public double ActualWidth { get; }

    public double ActualHeight { get; }

    FrameworkElement Element { get; }

    public event EventHandler? SizeChanged;

    public event EventHandler<MouseButtonEventArgsWrapper>? MouseDown;

    public event EventHandler<MouseButtonEventArgsWrapper>? MouseUp;

    public event EventHandler<MouseEventArgsWrapper>? MouseMove;

    public event EventHandler<MouseWheelEventArgsWrapper>? MouseWheel;
    bool CaptureMouse();
    void ReleaseMouseCapture();
    Point PointToScreen(Point point);

    Point PointFromScreen(Point point);
}