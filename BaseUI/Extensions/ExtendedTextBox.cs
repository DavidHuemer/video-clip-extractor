using System.Windows.Input;

namespace BaseUI.Extensions;

public class ExtendedTextBox : TextBox
{
    public ExtendedTextBox()
    {
        MouseLeave += OnMouseLeave;
        MouseEnter += OnMouseEnter;
    }

    private void OnMouseEnter(object sender, MouseEventArgs e)
    {
        var parentWindow = Window.GetWindow(this);

        if (parentWindow != null)
        {
            Mouse.RemovePreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
        }
    }

    private void OnMouseLeave(object sender, MouseEventArgs e)
    {
        var parentWindow = Window.GetWindow(this);

        if (parentWindow != null)
        {
            Mouse.AddPreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
        }
    }

    private void ParentWindow_OnMouseDown(object sender, MouseButtonEventArgs mouseButtonEventArgs)
    {
        var parentWindow = Window.GetWindow(this);

        if (parentWindow != null)
        {
            Mouse.RemovePreviewMouseDownHandler(parentWindow, ParentWindow_OnMouseDown);
        }

        Keyboard.ClearFocus();
    }
}