using JetBrains.Annotations;

namespace BaseUI.Services.WindowService.ActiveWindow;

[UsedImplicitly]
public class ActiveWindowManager : IActiveWindowManager
{
    /// <summary>
    /// The list of open windows.
    /// </summary>
    private readonly List<IWindow> _openWindows = [];

    public IWindow? ActiveWindow => _openWindows.LastOrDefault();

    public void AddWindow(IWindow window)
    {
        window.Closed += OnWindowClosed;
        _openWindows.Add(window);
    }

    private void OnWindowClosed(object? sender, EventArgs e)
    {
        if (sender is not IWindow window)
            return;

        window.Closed -= OnWindowClosed;
        _openWindows.Remove(window);
    }
}