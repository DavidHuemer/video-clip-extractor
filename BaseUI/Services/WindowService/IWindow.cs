namespace BaseUI.Services.WindowService;

public interface IWindow
{
    #region Properties

    public object DataContext { get; set; }

    #endregion

    void Show();

    bool? ShowDialog();

    void Close();

    event EventHandler? ContentRendered;
}