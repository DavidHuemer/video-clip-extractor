using BaseUI.ViewModels;

namespace VideoClipExtractor.Tests.BaseUI.Handler.ViewModelHandlerTests;

internal sealed class ExampleViewModel : BaseViewModel
{
    private string _exampleProperty = "";

    private string? _nullAble = "";

    public string ExampleProperty
    {
        get => _exampleProperty;
        set
        {
            _exampleProperty = value;
            OnPropertyChanged();
        }
    }

    public string? NullAble
    {
        get => _nullAble;
        set
        {
            _nullAble = value;
            OnPropertyChanged();
        }
    }

    public string NotNotify { get; set; } = "";
}