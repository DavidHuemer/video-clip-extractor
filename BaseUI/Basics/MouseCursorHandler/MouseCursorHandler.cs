using System.Runtime.InteropServices;
using System.Windows.Input;
using BaseUI.Basics.FrameworkElementWrapper;

namespace BaseUI.Basics.MouseCursorHandler;

public class MouseCursorHandler : IMouseCursorHandler
{
    public void SetCursorPosition(Point position) => SetCursorPos((int)position.X, (int)position.Y);
    public Point GetMousePosition(IFrameworkElement element) => Mouse.GetPosition(element.Element);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);
}