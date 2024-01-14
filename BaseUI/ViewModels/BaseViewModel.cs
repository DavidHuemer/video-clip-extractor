using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace BaseUI.ViewModels;

/// <summary>
///     Base class for all view models.
/// </summary>
public abstract class BaseViewModel : INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}