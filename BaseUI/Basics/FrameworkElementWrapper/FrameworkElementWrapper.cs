using BaseUI.Events;

namespace BaseUI.Basics.FrameworkElementWrapper;

public class FrameworkElementWrapper : IFrameworkElement
{
    private readonly FrameworkElement _element;

    public FrameworkElementWrapper(FrameworkElement element)
    {
        _element = element;
        element.SizeChanged += (sender, args) => SizeChanged?.Invoke(sender, args);
        element.MouseWheel += (sender, args) => MouseWheel?.Invoke(sender, new MouseWheelEventArgsWrapper(args.Delta));
    }

    public double ActualWidth => _element.ActualWidth;
    public event EventHandler? SizeChanged;
    public event EventHandler<MouseWheelEventArgsWrapper>? MouseWheel;
}