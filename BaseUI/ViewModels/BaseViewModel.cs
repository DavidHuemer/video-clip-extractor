using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BaseUI.ViewModels;

/// <summary>
///     Base class for all view models.
/// </summary>
public abstract class BaseViewModel : IBaseViewModel, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    protected void SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
    {
        if (Equals(backingField, value))
        {
            return;
        }

        backingField = value;
        OnPropertyChanged(propertyName);
    }
}