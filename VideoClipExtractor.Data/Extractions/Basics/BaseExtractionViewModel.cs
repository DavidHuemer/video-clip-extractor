using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.ViewModels;

namespace VideoClipExtractor.Data.Extractions.Basics;

public abstract class BaseExtractionViewModel : BaseViewModel, IExtractionViewModel
{
    private bool _isSelected;
    private Action<IExtractionViewModel>? _selectionCallback;

    public ICommand Select => new RelayCommand<string>(DoSelect, _ => true);

    public bool IsSelected
    {
        get => _isSelected;
        set
        {
            _isSelected = value;
            OnPropertyChanged();
        }
    }

    public virtual void SetupSelection(Action<IExtractionViewModel> selectionCallback)
    {
        _selectionCallback = selectionCallback;
    }

    private void DoSelect(string? obj) => _selectionCallback?.Invoke(this);
}