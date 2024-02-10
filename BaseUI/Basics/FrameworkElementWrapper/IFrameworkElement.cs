using BaseUI.Events;

namespace BaseUI.Basics.FrameworkElementWrapper;

public interface IFrameworkElement
{
    public double ActualWidth { get; }

    public event EventHandler? SizeChanged;

    public event EventHandler<MouseWheelEventArgsWrapper>? MouseWheel;
}