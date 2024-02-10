using BaseUI.Events;

namespace BaseUI.Basics.FrameworkElementWrapper;

public class FrameworkElementWrapper : IFrameworkElement
{
    public FrameworkElementWrapper(FrameworkElement element)
    {
        Element = element;
        element.SizeChanged += (sender, args) => SizeChanged?.Invoke(sender, args);
        element.MouseWheel += (sender, args) => MouseWheel?.Invoke(sender, new MouseWheelEventArgsWrapper(args.Delta));
        element.MouseDown += (sender, args) =>
            MouseDown?.Invoke(sender, new MouseButtonEventArgsWrapper(args.ChangedButton, args.GetPosition));
        element.MouseUp += (sender, args) =>
            MouseUp?.Invoke(sender, new MouseButtonEventArgsWrapper(args.ChangedButton, args.GetPosition));
        element.MouseMove += (sender, args) => MouseMove?.Invoke(sender, new MouseEventArgsWrapper(args.GetPosition));
    }

    public double ActualWidth => Element.ActualWidth;
    public double ActualHeight => Element.ActualHeight;

    public event EventHandler? SizeChanged;
    public event EventHandler<MouseButtonEventArgsWrapper>? MouseDown;
    public event EventHandler<MouseButtonEventArgsWrapper>? MouseUp;
    public event EventHandler<MouseEventArgsWrapper>? MouseMove;
    public event EventHandler<MouseWheelEventArgsWrapper>? MouseWheel;

    public FrameworkElement Element { get; }

    public bool CaptureMouse() => Element.CaptureMouse();

    public void ReleaseMouseCapture() => Element.ReleaseMouseCapture();
    public Point PointToScreen(Point point) => Element.PointToScreen(point);
}