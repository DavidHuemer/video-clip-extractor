using BaseUI.Services.WindowService;

namespace VideoClipExtractor.Tests.BaseUI.Services.WindowServiceTests;

public class ExampleWindow : IWindow
{
    public object DataContext { get; set; }

    public void Show()
    {
        throw new NotImplementedException();
    }

    public bool? ShowDialog()
    {
        throw new NotImplementedException();
    }

    public void Close()
    {
        throw new NotImplementedException();
    }

    public event EventHandler? ContentRendered;
    public event EventHandler? Closed;
}