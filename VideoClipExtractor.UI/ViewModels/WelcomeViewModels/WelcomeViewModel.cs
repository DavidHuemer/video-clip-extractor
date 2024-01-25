using System.Windows.Input;
using BaseUI.Commands;
using BaseUI.ViewModels;

namespace VideoClipExtractor.UI.ViewModels.WelcomeViewModels;

public class WelcomeViewModel : BaseViewModel
{
    #region Events

    public event EventHandler? NewProjectRequested;

    public event EventHandler? OpenProjectRequested;

    #endregion

    #region Commands

    public ICommand NewProject => new RelayCommand<string>(DoNewProject, _ => true);

    private void DoNewProject(string? obj)
    {
        NewProjectRequested?.Invoke(null, EventArgs.Empty);
    }

    public ICommand OpenProject => new RelayCommand<string>(DoOpenProject, _ => true);

    private void DoOpenProject(string? obj)
    {
        OpenProjectRequested?.Invoke(null, EventArgs.Empty);
    }

    #endregion
}