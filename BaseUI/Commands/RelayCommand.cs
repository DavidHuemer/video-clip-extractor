using System.Windows.Input;

namespace BaseUI.Commands;

public class RelayCommand<T>(Action<T?> execute, Predicate<T?>? canExecute = null) : ICommand
{
    private readonly Action<T?> _execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        if (parameter == null && typeof(T).IsValueType)
            return canExecute == null || canExecute(default);

        return canExecute == null || canExecute((T)parameter!);
    }

    public void Execute(object? parameter)
    {
        if (parameter == null && typeof(T).IsValueType)
            _execute(default);
        else
            _execute((T)parameter!);
    }
}