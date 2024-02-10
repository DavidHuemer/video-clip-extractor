using System.Runtime.InteropServices;

namespace BaseUI.Basics.MouseCursorHandler;

public class MouseCursorHandler : IMouseCursorHandler
{
    public void SetCursorPosition(Point position) => SetCursorPos((int)position.X, (int)position.Y);

    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    private static extern bool SetCursorPos(int x, int y);
}