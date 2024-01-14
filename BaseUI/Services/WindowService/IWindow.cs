namespace BaseUI.Services.WindowService;

public interface IWindow
{
    void Close();

    event EventHandler? ContentRendered;
}